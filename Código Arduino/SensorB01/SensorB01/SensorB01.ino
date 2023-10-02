/*
 * SENSOR CAPACITIVO DE HUMEDAD DE SUELO
 *  HW-390
 *  PIN A2 => Salida AUOT  Sensor H1
 *  PIN A1 => Salida AUOT  Sensor H2
 *  PIN A0 => Salida AUOT  Sensor H3
 *  ACC => 5V
 *  
 * SENSOR DE TEMPERATURA
 *  DS18B20 
 *  PIN 9 => DQ
 *  ACC => 5V
 * 
 *  
 *  MODULO BLUETOOTH HC-05
 *  VCC => 5V
 *  PIN 2 => TXD
 *  PIN 3 => RXD
 *  
 *  MODULO RELOJ DE TIEMPO REAL (RTC)
 *  DS3231
 *  VCC => 5V 
 *  PIN A5 =>
 *  PIN A4 =>
 * 
 *  MODULO MICROSD
 *  VCC => 5V
 *  PIN 12 => MISO
 *  PIN 11 => MOSI
 *  PIN 10 => CS
 *  PIN 13 => SCK
 *  Verificar que la memoria a usar este en formato FAT (sistema de archivo: FAT)
 * 
 *  MEDIDOR DE VOLTAJE POR PIN ANALÓGICO
 *  PIN A3 => positivo de las baterias voltaje(3,6 a 4,2V)
 *  
 *  RESISTENCIA PULL-DOWN
 *  PIN 7 => Extremo del botón que va al GND del modulo Bluetooth
 *
 */

#include <SPI.h>
#include <SdFat.h>
#include <Wire.h>
#include <RTClib.h>
#include <OneWire.h>
#include <DallasTemperature.h>
#include <EEPROM.h>
#include <SoftwareSerial.h>
#include <LowPower.h>

#define SSpin 10
#define PIN_TEMPERATURA 9
#define SENSOR_HUMEDAD_1 A2
#define SENSOR_HUMEDAD_2 A1
#define SENSOR_HUMEDAD_3 A0
#define PIN_VOLTAGE A3
#define VOLTAJE_MAXIMO 4.2
#define VOLTAJE_MINIMO 3.6
#define EEPROM_ADDRESS 0
#define PIN_BLUETOOTH 7
#define BT_RX 2
#define BT_TX 3
SoftwareSerial bluetooth(BT_RX, BT_TX);

RTC_DS3231 rtc;
SdFat SD;
File32 archivo;
char comando[20];
byte indexComando = 0;
bool guardando = false;
unsigned long intervaloGuardado = 0;
unsigned long ultimoGuardado = 0;

OneWire oneWire(PIN_TEMPERATURA);
DallasTemperature sensort(&oneWire);
float temperatura;
int humedad1, humedad2, humedad3;
char deviceName[7] = "B01000";

void setup() {

  bluetooth.begin(9600);
  pinMode(PIN_BLUETOOTH, INPUT);
  for (int i = 0; i < 6; i++) {
    deviceName[i] = EEPROM.read(EEPROM_ADDRESS + i);
  }
  deviceName[6] = '\0';
  sensort.begin();
}

void loop() {

  if (guardando && (millis() - ultimoGuardado >= intervaloGuardado)) {
    guardarDatosSD();
    ultimoGuardado = millis();
  }

  if (digitalRead(PIN_BLUETOOTH) == HIGH) {
    LowPower.powerDown(SLEEP_8S, ADC_OFF, BOD_OFF);  // Duerme por 8 segundos
    ultimoGuardado -= 8000;
  } else {
    while (bluetooth.available()) {
      char inChar = (char)bluetooth.read();
      if (inChar == '\n') {
        comando[indexComando] = '\0';
        procesarComando();
        indexComando = 0;
      } else if (indexComando < 19) {
        comando[indexComando++] = inChar;
      }
    }
  }
}

void tomarLecturas() {
  sensort.requestTemperatures();
  temperatura = sensort.getTempCByIndex(0);
  humedad1 = analogRead(SENSOR_HUMEDAD_1);
  humedad2 = analogRead(SENSOR_HUMEDAD_2);
  humedad3 = analogRead(SENSOR_HUMEDAD_3);
}

void guardarDatosSD() {
  tomarLecturas();

  char nombreArchivo[11];
  generarNombreArchivo(nombreArchivo);
  archivo = SD.open(nombreArchivo, FILE_WRITE);

  if (archivo) {
    DateTime ahora = rtc.now();
    char cadenaDatos[70];
    char cadenaTemperatura[10];
    dtostrf(temperatura, 5, 2, cadenaTemperatura);
    snprintf(cadenaDatos, sizeof(cadenaDatos), "%02d;%02d;%02d;%02d;%02d;%02d;%d;%d;%d;%s", ahora.day(), ahora.month(), ahora.year() - 2000, ahora.hour(), ahora.minute(), ahora.second(), humedad1, humedad2, humedad3, cadenaTemperatura);
    archivo.println(cadenaDatos);
    archivo.close();
  }
}

void generarNombreArchivo(char* nombre) {
  DateTime now = rtc.now();
  snprintf(nombre, 10, "%02d%02d%02d.TXT", now.day(), now.month(), now.year() - 2000);
}

void procesarComando() {
  //bluetooth.print(F("Cmd: "));
  //bluetooth.println(comando);

  if (strncmp(comando, "i ", 2) == 0) {
    leerDatosDeFecha(comando + 2);
  } else if (strcmp(comando, "iT") == 0) {
    leerTodosLosDatos();
  } else if (strncmp(comando, "g ", 2) == 0) {
    int horas = (comando[2] - '0') * 10 + (comando[3] - '0');
    int minutos = (comando[4] - '0') * 10 + (comando[5] - '0');
    int segundos = (comando[6] - '0') * 10 + (comando[7] - '0');
    intervaloGuardado = (horas * 3600L + minutos * 60L + segundos) * 1000L;
    guardando = true;
    ultimoGuardado = millis();
    guardarDatosSD();
  } else if (strcmp(comando, "r") == 0) {
    tomarLecturas();
    char cadenaTemperatura[10];
    dtostrf(temperatura, 5, 1, cadenaTemperatura);  // Convertir temperatura a cadena con un decimal
    // Enviar datos por Bluetooth
    char cadenaBluetooth[50];
    snprintf(cadenaBluetooth, sizeof(cadenaBluetooth), "l%03d;%03d;%03d;%s&", humedad1, humedad2, humedad3, cadenaTemperatura);
    bluetooth.println(cadenaBluetooth);
  } else if (strcmp(comando, "d") == 0) {
    guardando = false;
  } else if (strcmp(comando, "ls") == 0) {
    listarArchivos();
  } else if (strncmp(comando, "a ", 2) == 0) {
    ajustarRTC(comando + 2);
  } else if (strncmp(comando, "e ", 2) == 0) {
    eliminarArchivoDeFecha(comando + 2);
  } else if (strcmp(comando, "l") == 0) {
    limpiarSD();
  } else if (strcmp(comando, "s") == 0) {
    mostrarEstado();
  } else if (strncmp(comando, "n ", 2) == 0) {
    setDeviceName(comando + 2);
  } else if (strcmp(comando, "m") == 0) {
    getDeviceName();
  } else {
    bluetooth.println("u0&");
  }
}

void mostrarEstado() {
  int estadoReloj = rtc.begin() ? 1 : 0;
  int estadoSD = SD.begin(SSpin) ? 1 : 0;
  int estadoGuardado = guardando ? 1 : 0;

  uint32_t cardSize = SD.card()->sectorCount() / (2048);
  uint32_t freeSpace = SD.vol()->freeClusterCount() * SD.vol()->sectorsPerCluster() / 2048;
  int porcentajeLibre = (freeSpace * 100) / cardSize;
  int porcentajeBateria = calcularPorcentajeBateria();

  char cadenaBluetooth[50];
  snprintf(cadenaBluetooth, sizeof(cadenaBluetooth), "e%d;%d;%d;%03d;%03d&", estadoReloj, estadoSD, estadoGuardado, porcentajeLibre, porcentajeBateria);
  bluetooth.println(cadenaBluetooth);
}

void leerDatosDeFecha(char* fecha) {
  char nombreArchivo[10];
  snprintf(nombreArchivo, sizeof(nombreArchivo), "%s.TXT", fecha);
  leerArchivo(nombreArchivo);
}

void leerArchivo(char* nombreArchivo) {
  archivo = SD.open(nombreArchivo, FILE_READ);
  if (archivo) {
    bluetooth.println("aa");
    bluetooth.print("i");
    bluetooth.print(deviceName);
    bluetooth.print(";");
    bluetooth.println(String(nombreArchivo).substring(0, 6));
    while (archivo.available()) {
      bluetooth.print("y");
      bluetooth.println(archivo.readStringUntil('\n'));
    }
    archivo.close();
    bluetooth.println("az");
  } else {
    bluetooth.println("v0&");
  }
}

void leerTodosLosDatos() {
  File32 root = SD.open("/");
  char filename[13];
  int contadorArchivos = 0;

  while (true) {
    File32 entry = root.openNextFile();
    if (!entry) {
      break;
    }
    if (!entry.isDirectory()) {
      entry.getName(filename, sizeof(filename));
      contadorArchivos++;
      leerArchivo(filename);
    }
    entry.close();
  }
  root.close();
}

void limpiarSD() {
  File32 root = SD.open("/");
  eliminarRecursivamente(root);
  root.close();
  bluetooth.println("c1&");
}

void eliminarRecursivamente(File32 dir) {
  if (!dir) {
    return;
  }

  char filename[13];
  File32 entry;
  while ((entry = dir.openNextFile())) {
    entry.getName(filename, sizeof(filename));
    if (entry.isDirectory()) {
      eliminarRecursivamente(entry);
      SD.rmdir(filename);
    } else {
      SD.remove(filename);
    }
    entry.close();
  }
}

void eliminarArchivoDeFecha(char* fecha) {
  char nombreArchivo[10];
  snprintf(nombreArchivo, sizeof(nombreArchivo), "%s.TXT", fecha);
  if (SD.exists(nombreArchivo)) {
    SD.remove(nombreArchivo);
    bluetooth.println("q1&");
  } else {
    bluetooth.println("q0&");
  }
}

void listarArchivos() {
  File32 root = SD.open("/");
  char filename[13];
  int contadorArchivos = 0;
  String cadenaBluetooth = "r";

  while (true) {
    File32 entry = root.openNextFile();
    if (!entry) {
      break;
    }
    if (!entry.isDirectory()) {
      entry.getName(filename, sizeof(filename));
      String nombre = String(filename).substring(0, 6);
      cadenaBluetooth += ";" + nombre;
      contadorArchivos++;
    }
    entry.close();
  }
  root.close();

  cadenaBluetooth = "r" + String(contadorArchivos) + cadenaBluetooth + "&";
  bluetooth.println(cadenaBluetooth);
}

void ajustarRTC(char* fechaHora) {
  int dia = (fechaHora[0] - '0') * 10 + (fechaHora[1] - '0');
  int mes = (fechaHora[2] - '0') * 10 + (fechaHora[3] - '0');
  int ano = 2000 + (fechaHora[4] - '0') * 10 + (fechaHora[5] - '0');
  int horas = (fechaHora[6] - '0') * 10 + (fechaHora[7] - '0');
  int minutos = (fechaHora[8] - '0') * 10 + (fechaHora[9] - '0');

  rtc.adjust(DateTime(ano, mes, dia, horas, minutos, 0));
  bluetooth.println("j1&");
}

float leerVoltaje() {
  float voltaje = analogRead(PIN_VOLTAGE) * (5.0 / 1023.0);
  return voltaje;
}

int calcularPorcentajeBateria() {
  float voltaje = leerVoltaje();
  if (voltaje >= VOLTAJE_MAXIMO) return 100;
  if (voltaje <= VOLTAJE_MINIMO) return 0;
  return (int)((voltaje - VOLTAJE_MINIMO) * 100 / (VOLTAJE_MAXIMO - VOLTAJE_MINIMO));
}

void setDeviceName(char* newName) {
  strncpy(deviceName, newName, sizeof(deviceName) - 1);
  deviceName[sizeof(deviceName) - 1] = '\0';
  EEPROM.put(0, deviceName);
}

void getDeviceName() {
  EEPROM.get(0, deviceName);
  char cadenaBluetooth[10];
  snprintf(cadenaBluetooth, sizeof(cadenaBluetooth), "n%s&", deviceName);
  bluetooth.println(cadenaBluetooth);
}