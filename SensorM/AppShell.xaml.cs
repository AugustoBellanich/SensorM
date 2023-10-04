using SensorM.Pages;

namespace SensorM
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BluetoothDevicesPage), typeof(BluetoothDevicesPage));  // Registramos la ruta de la página BluetoothDevicesPage
            Routing.RegisterRoute(nameof(TestPage), typeof(TestPage));  // Registramos la ruta de la página TestPage
        }
    }
}