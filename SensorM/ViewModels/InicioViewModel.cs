using Microsoft.Maui.Controls;
using SensorM.Interfaces;
using SQLiteDatabase.Models;
using SensorM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SensorM.Pages;
using CommunityToolkit.Mvvm.Input;

namespace SensorM.ViewModels
{
    public partial class InicioViewModel : BaseViewModel
    {
        private readonly IThemeService _themeService;
        private bool _isDarkMode;


        public ObservableCollection<Dispositivo> Dispositivos { get; } = new ObservableCollection<Dispositivo>();

        public InicioViewModel(IThemeService themeService, INavigationService navigationService) : base(navigationService)
        {
            _themeService = themeService;

            IsDarkMode = _themeService.CurrentTheme == Theme.Dark;
        }

        [RelayCommand]
        public void NavigateToBluetoothDevicesPage()
        {
            _navigationService.NavigateToAsync(nameof(BluetoothDevicesPage));
        }

        [RelayCommand]
        public void NavigateToTestPage()
        {
            _navigationService.NavigateToAsync(nameof(TestPage));
        }

        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                SetProperty(ref _isDarkMode, value);
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


