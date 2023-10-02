using SensorM.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SensorM.ViewModels
{
    public class BluetoothViewModel : BaseViewModel
    {
        private readonly IBluetoothService _bluetoothService;
        private ObservableCollection<string> _dispositivos;
        private string _selectedDevice;
        private string _receivedData;

        public BluetoothViewModel()
        {
            _bluetoothService = DependencyService.Get<IBluetoothService>();
            BuscarDispositivosCommand = new Command(async () => await BuscarDispositivos());
            ConectarCommand = new Command(async () => await Conectar());
            EnviarDatosCommand = new Command(async () => await EnviarDatos());
        }

        public ICommand BuscarDispositivosCommand { get; }
        public ICommand ConectarCommand { get; }
        public ICommand EnviarDatosCommand { get; }

        public ObservableCollection<string> Dispositivos
        {
            get => _dispositivos;
            set => SetProperty(ref _dispositivos, value);
        }

        public string SelectedDevice
        {
            get => _selectedDevice;
            set => SetProperty(ref _selectedDevice, value);
        }

        public string ReceivedData
        {
            get => _receivedData;
            set => SetProperty(ref _receivedData, value);
        }

        private async Task BuscarDispositivos()
        {
            var dispositivos = await _bluetoothService.BuscarDispositivosAsync();
            Dispositivos = new ObservableCollection<string>(dispositivos);
        }

        private async Task Conectar()
        {
            if (string.IsNullOrEmpty(SelectedDevice)) return;
            bool isConnected = await _bluetoothService.ConectarAsync(SelectedDevice);
            if (isConnected)
            {
                // Conexión exitosa
            }
        }

        private async Task EnviarDatos()
        {
            string data = "Datos a enviar";
            await _bluetoothService.EnviarDatosAsync(data);
        }
    }

}
