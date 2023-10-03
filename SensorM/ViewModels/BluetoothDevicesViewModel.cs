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
    public class BluetoothDevicesViewModel : BaseViewModel
    {
        private readonly IBluetoothService _bluetoothService;
        private ObservableCollection<string> _dispositivos;
        private string _selectedDevice;
        private string _statusMessage;

        public BluetoothDevicesViewModel(IBluetoothService bluetoothService, INavigationService navigationService): base(navigationService)
        {
            _bluetoothService = bluetoothService ?? throw new ArgumentNullException(nameof(bluetoothService));
            BuscarDispositivosCommand = new Command(async () => await BuscarDispositivos());
            ConectarCommand = new Command(async () => await Conectar());
        }

        public ICommand BuscarDispositivosCommand { get; }
        public ICommand ConectarCommand { get; }

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

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private async Task BuscarDispositivos()
        {
            try
            {
                IsBusy = true;
                var dispositivos = await _bluetoothService.BuscarDispositivosAsync();
                Dispositivos = new ObservableCollection<string>(dispositivos);
                StatusMessage = Dispositivos.Count > 0 ? "Dispositivos encontrados" : "No se encontraron dispositivos";
            }
            catch (Exception ex)
            {
                StatusMessage = "Error al buscar dispositivos: " + ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task Conectar()
        {
            if (string.IsNullOrEmpty(SelectedDevice))
            {
                StatusMessage = "Por favor, selecciona un dispositivo para conectarte.";
                return;
            }

            try
            {
                IsBusy = true;
                bool isConnected = await _bluetoothService.ConectarAsync(SelectedDevice);
                StatusMessage = isConnected ? "Conectado a " + SelectedDevice : "No se pudo conectar a " + SelectedDevice;
            }
            catch (Exception ex)
            {
                StatusMessage = "Error al intentar conectar: " + ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

}
