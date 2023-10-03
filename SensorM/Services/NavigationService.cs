using SensorM.Interfaces;
using SensorM.Pages;
using SensorM.ViewModels;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SensorM.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Page _currentPage;
        private readonly IBluetoothService _bluetoothService;

        public NavigationService(Page currentPage, IBluetoothService bluetoothService)
        {
            _currentPage = currentPage ?? throw new ArgumentNullException(nameof(currentPage));
            _bluetoothService = bluetoothService ?? throw new ArgumentNullException(nameof(bluetoothService));
        }

        public async Task NavigateToPage(string pageName, object parameter = null)
        {
            Page page = null;

            switch (pageName)
            {
                case "BluetoothDevicesPage":
                    // Crear una instancia de tu ViewModel
                    var bluetoothViewModel = new BluetoothDevicesViewModel(_bluetoothService);
                    // Pasar el ViewModel al constructor de tu página
                    page = new BluetoothDevicesPage(bluetoothViewModel);
                    break;
                    // Agrega más casos según las páginas que tengas
                    // case "OtraPagina":
                    //     page = new OtraPagina();
                    //     break;
            }

            if (page != null)
            {
                await _currentPage.Navigation.PushAsync(page);
            }
            else
            {
                throw new ArgumentException($"No se puede navegar a la página: {pageName}. Asegúrate de que el nombre de la página esté escrito correctamente y que hayas agregado un caso para ella en el servicio de navegación.");
            }
        }

        public async Task NavigateBack()
        {
            await _currentPage.Navigation.PopAsync();
        }
    }
}