using Microsoft.Maui.Controls;
using SensorM.Interfaces;
using SQLiteDatabase.Models;
using SensorM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SensorM.Pages;
using CommunityToolkit.Mvvm.Input;
using SensorM.Events;

namespace SensorM.ViewModels
{
    public partial class InicioViewModel : BaseViewModel
    {
        private readonly IThemeService _themeService;
        private bool _isDarkMode;
        private readonly IBluetoothService _bluetoothService;

        private string _connectedDeviceName;

        public ObservableCollection<Dispositivo> Dispositivos { get; } = new ObservableCollection<Dispositivo>();

        public InicioViewModel(IThemeService themeService, IBluetoothService bluetoothService, INavigationService navigationService) : base(navigationService)
        {
            _themeService = themeService;
            _bluetoothService = bluetoothService;
            IsDarkMode = _themeService.CurrentTheme == Theme.Dark;

            UpdateBluetoothStatus();
        }

        public void UpdateBluetoothStatus()
        {
            IsConnected = _bluetoothService.IsConnected; // Asegúrate de que tienes un método/propiedad IsConnected en tu servicio Bluetooth
            ConnectedDeviceName = _bluetoothService.ConnectedDeviceName; // Similar para el nombre del dispositivo
        }

        [RelayCommand]
        private async Task DisconnectBluetooth()
        {
            // Lógica para desconectar el dispositivo Bluetooth.
            await _bluetoothService.DesconectarAsync();
            UpdateBluetoothStatus();
        }

        private bool _isConnected;


        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }


        public string ConnectedDeviceName
        {
            get => _connectedDeviceName;
            set => SetProperty(ref _connectedDeviceName, value);
        }



        [RelayCommand]
        public async Task NavigateToBluetoothDevicesPageAsync()
        {
            //Solicitar los permisos de Bluetooth
            var granted = await RequestBluetoothPermissionsAsync();

            if (!granted)
            {
                await _navigationService.NavigateToAsync(nameof(BluetoothDevicesPage));
            }
            else
            {
                await _navigationService.NavigateToAsync(nameof(BluetoothDevicesPage));
            }

            
        }


        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                if (_isDarkMode != value)
                {
                    _isDarkMode = value;
                    OnPropertyChanged();

                    // Cambia el tema cuando IsDarkMode cambia
                    _themeService.SetTheme(_isDarkMode ? Theme.Dark : Theme.Light);
                }
            }
        }


        //Comando para ir a la pagina de lectura de datos del sensor B01
        [RelayCommand]
        public async Task NavigationSensorB01Page()
        {
            await _navigationService.NavigateToAsync(nameof(SensorB01Page));
        }

        [RelayCommand]
        private void RegisterDevice()
        {
            // Lógica para registrar el dispositivo, por ejemplo:
            // - Validar el nombre del dispositivo
            // - Guardar el dispositivo en una base de datos local o en la nube
            // - Actualizar la propiedad IsDeviceRegistered según sea necesario
        }


        private async Task<bool> RequestBluetoothPermissionsAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            return status == PermissionStatus.Granted;
        }

    }
}


