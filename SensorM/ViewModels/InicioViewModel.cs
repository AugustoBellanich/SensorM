using Microsoft.Maui.Controls;
using SensorM.Interfaces;
using SQLiteDatabase.Models;
using SensorM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SensorM.Pages;

namespace SensorM.ViewModels
{
    public class InicioViewModel : BaseViewModel
    {
        private readonly IThemeService _themeService;
        private bool _isDarkMode;

        private readonly INavigationService _navigationService; // Agregado para gestionar la navegación

        public ICommand BluetoothCommand { get; }
        public ObservableCollection<Dispositivo> Dispositivos { get; } = new ObservableCollection<Dispositivo>();

        public InicioViewModel(IThemeService themeService, INavigationService navigationService)
        {
            _themeService = themeService;
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            IsDarkMode = _themeService.CurrentTheme == Theme.Dark;
            BluetoothCommand = new Command(async () => await OnBluetoothPressed());
        }

        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                SetProperty(ref _isDarkMode, value);
            }
        }

        private async Task OnBluetoothPressed()
        {
            var tienePermiso = await RequestBluetoothPermissionsAsync();

            if (tienePermiso)
            {
                // Navegación a la página de dispositivos Bluetooth
                await _navigationService.NavigateToPage("BluetoothDevicesPage");
            }
            else
            {
                // Informar al usuario que necesita otorgar permisos
                // Puedes usar un DisplayAlert o algún otro mecanismo para mostrar un mensaje al usuario
            }
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


