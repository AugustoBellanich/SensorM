using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls;
using SensorM.Data;
using SensorM.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;

namespace SensorM.ViewModels
{
    public partial class SensorB01ViewModel: BaseViewModel
    {
        private IBluetoothService _bluetoothService; // Asumiendo que tienes un servicio de Bluetooth

        private System.Timers.Timer _dataRequestTimer; // Añadir esta línea

        public SensorB01ViewModel(IBluetoothService bluetoothService, INavigationService navigationService) : base(navigationService)
        {
            _bluetoothService = bluetoothService;

            // Inicializar y configurar el temporizador
            _dataRequestTimer = new System.Timers.Timer(1000); // Intervalo de 5 segundos, ajusta según sea necesario
            _dataRequestTimer.Elapsed += OnDataRequestTimerElapsed;
            _dataRequestTimer.Start();
        }

        private SensorB01Data _data;

        public SensorB01Data Data
        {
            get => _data;
            set
            {
                SetProperty(ref _data, value);
            }
        }

        private async void OnDataRequestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            await RequestSensorDataAsync();
            UpdateGauges();
        }

        [RelayCommand]
        public async Task RequestSensorDataAsync()
        {
            // Enviar el comando "r" para solicitar datos del sensor
            await _bluetoothService.EnviarDatosAsync("r");

            // Esperar y recibir la respuesta
            string rawData = await _bluetoothService.RecibirDatosAsync();

            // Procesar y actualizar la UI
            SensorB01Data parsedData = ParseSensorB01Data(rawData);
            if (parsedData != null)
            {
                Data = parsedData;
            }
            else
            {
                // Puedes manejar el caso en que no se recibieron datos válidos si es necesario.
            }
        }


        public SensorB01Data ParseSensorB01Data(string message)
        {
            try
            {
                // Validar la entrada
                if (string.IsNullOrWhiteSpace(message) || !message.StartsWith("l") || !message.EndsWith("&\r\n"))
                {
                    throw new FormatException("Los datos recibidos no tienen el formato esperado.");
                }

                // Extraer la parte útil de la cadena
                string usefulData = message[1..^3]; // Elimina el primer y los dos últimos caracteres ("l" y "&\r\n")

                // Dividir los datos en sus partes componentes
                string[] parts = usefulData.Split(';');
                if (parts.Length != 4)
                {
                    throw new FormatException("Los datos recibidos no tienen el formato esperado.");
                }

                // Convertir las partes de la cadena a los tipos de datos apropiados
                if (!double.TryParse(parts[0], out double capacitancia1) ||
                    !double.TryParse(parts[1], out double capacitancia2) ||
                    !double.TryParse(parts[2], out double capacitancia3) ||
                    !double.TryParse(parts[3], out double temperatura))
                {
                    throw new FormatException("Los datos recibidos no pueden ser convertidos a números.");
                }

                // Crear y devolver un objeto SensorB01Data con los datos parseados
                var sensorData = new SensorB01Data
                {
                    Capacitancia1 = capacitancia1,
                    Capacitancia2 = capacitancia2,
                    Capacitancia3 = capacitancia3,
                    Temperatura = temperatura
                };

                sensorData.CalcularHumedades(); // Calcular los valores de humedad basados en las capacitancias
                return sensorData;
            }
            catch (FormatException ex)
            {
                // Aquí puedes manejar o registrar errores de formato
                Console.WriteLine($"Error de formato en mensaje: {message}. Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar o registrar otros errores
                Console.WriteLine($"Error desconocido en mensaje: {message}. Error: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<ISeries> Humedad1Series { get; set; }
        public IEnumerable<ISeries> Humedad2Series { get; set; }
        public IEnumerable<ISeries> Humedad3Series { get; set; }

        private void UpdateGauges()
        {
            // Asumiendo que tienes un método BuildSolidGauge que acepta un valor y devuelve una serie de datos
            Humedad1Series = GaugeGenerator.BuildSolidGauge(new GaugeItem(Convert.ToInt32(Data?.Humedad1 ?? 0), series =>       // the series style
            {
                series.Fill = new SolidColorPaint(SkiaSharp.SKColors.DodgerBlue);
                series.MaxRadialColumnWidth = 50;
                series.DataLabelsSize = 50;
            }));

            Humedad2Series = GaugeGenerator.BuildSolidGauge(new GaugeItem(Convert.ToInt32(Data?.Humedad2 ?? 0), series =>       // the series style
            {
                series.Fill = new SolidColorPaint(SkiaSharp.SKColors.DodgerBlue);
                series.MaxRadialColumnWidth = 50;
                series.DataLabelsSize = 50;
            }));
            Humedad3Series = GaugeGenerator.BuildSolidGauge(new GaugeItem(Convert.ToInt32(Data?.Humedad3 ?? 0), series =>       // the series style
            {
                series.Fill = new SolidColorPaint(SkiaSharp.SKColors.DodgerBlue);
                series.MaxRadialColumnWidth = 50;
                series.DataLabelsSize = 50;
            }));

            // Notificar a la vista que las series de datos han cambiado
            OnPropertyChanged(nameof(Humedad1Series));
            OnPropertyChanged(nameof(Humedad2Series));
            OnPropertyChanged(nameof(Humedad3Series));
        }
    }
}
