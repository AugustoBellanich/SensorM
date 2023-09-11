using Microsoft.Maui.Controls;
using SensorM.Interface;
using SensorM.Models;
using SensorM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SensorM.ViewModels
{
    public class InicioViewModel : BaseViewModel
    {
        private readonly IThemeService _themeService;
        private bool _isDarkMode;

        public InicioViewModel(IThemeService themeService)
        {
            _themeService = themeService;
            IsDarkMode = _themeService.CurrentTheme == Theme.Dark;

            BuscarDispositivosCommand = new Command(OnBuscarDispositivos);
        }

        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                SetProperty(ref _isDarkMode, value);
            }
        }

        public ObservableCollection<Dispositivo> Dispositivos { get; } = new ObservableCollection<Dispositivo>();
        public ICommand BuscarDispositivosCommand { get; }
        private void OnBuscarDispositivos()
        {
            // Aquí es donde colocarías tu lógica para buscar dispositivos
            // Por ejemplo, podrías iniciar una búsqueda de dispositivos Bluetooth
            // y luego agregarlos a la lista Dispositivos
        }
    }
}


