using SensorM.Pages;

namespace SensorM
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BluetoothDevicesPage), typeof(BluetoothDevicesPage));  // Registramos la ruta de la página BluetoothDevicesPage
            Routing.RegisterRoute(nameof(SensorB01Page), typeof(SensorB01Page));  // Registramos la ruta de la página SensorB01Page
        }
    }
}