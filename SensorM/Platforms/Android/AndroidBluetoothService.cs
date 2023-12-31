﻿using Android.Bluetooth;
using Android.Content;
using SensorM.Interfaces;
using Java.Util;
using System.Text;
using SensorM.Platforms.Android;


namespace SensorM.Platforms.Android
{
    public class AndroidBluetoothService : IBluetoothService
    {
        // Definir un UUID único para tu aplicación
        private static readonly UUID MY_UUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");
        private BluetoothSocket _socket;
        private Stream _inputStream;
        private Stream _outputStream;



        private string _connectedDeviceName;

        public bool IsConnected => _socket?.IsConnected ?? false;

        public string ConnectedDeviceName => _connectedDeviceName;



        public async Task<IEnumerable<string>> BuscarDispositivosAsync()
        {
            // Obtener el adaptador Bluetooth predeterminado
            BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (bluetoothAdapter == null)
            {
                // El dispositivo no soporta Bluetooth
                throw new NotSupportedException("El dispositivo no soporta Bluetooth");
            }

            if (!bluetoothAdapter.IsEnabled)
            {
                // Solicitar al usuario que habilite Bluetooth
                // Esto debería hacerse en la actividad o fragmento de Android
                Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                Platform.CurrentActivity.StartActivityForResult(enableBtIntent, 1);
                // Esperar a que el usuario habilite Bluetooth
                await Task.Delay(5000);
            }

            // Obtener la lista de dispositivos emparejados
            var pairedDevices = bluetoothAdapter.BondedDevices;
            if (pairedDevices.Count > 0)
            {
                return pairedDevices.Select(device => device.Name).ToList();
            }

            return new List<string>();
        }

        public async Task<bool> ConectarAsync(string deviceName)
        {
            BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            BluetoothDevice device = bluetoothAdapter.BondedDevices.FirstOrDefault(d => d.Name == deviceName);

            if (device == null)
            {
                throw new ArgumentException($"No se encontró el dispositivo {deviceName}");
            }

            _socket = device.CreateRfcommSocketToServiceRecord(MY_UUID);

            await _socket.ConnectAsync();

            _inputStream = _socket.InputStream;
            _outputStream = _socket.OutputStream;

            if(_socket.IsConnected)
            {
                _connectedDeviceName = deviceName;  // Guardamos el nombre del dispositivo conectado

            }

            return _socket.IsConnected;

        }



        public async Task EnviarDatosAsync(string comando)
        {
            try
            {
                if (_outputStream == null || !_socket.IsConnected)
                {
                    throw new InvalidOperationException("No está conectado a un dispositivo Bluetooth");
                }

                // Asegurarte de que el comando termina con '\n'
                if (!comando.EndsWith("\n"))
                {
                    comando += "\n";
                }

                // Convertir la cadena a bytes
                byte[] buffer = Encoding.UTF8.GetBytes(comando);

                await _outputStream.WriteAsync(buffer, 0, buffer.Length);


            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al enviar datos al dispositivo Bluetooth", ex);
            }

           
        }

        public async Task<string> RecibirDatosAsync()
        {
            if (_inputStream == null || !_socket.IsConnected)
            {
                throw new InvalidOperationException("No está conectado a un dispositivo Bluetooth");
            }

            byte[] buffer = new byte[1024];
            int bytesRead = await _inputStream.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        public Task DesconectarAsync()
        {
            _inputStream?.Close();
            _outputStream?.Close();
            _socket?.Close();

            _connectedDeviceName = null;  // Limpiamos el nombre del dispositivo al desconectar

            return Task.CompletedTask;
        }
    }
}