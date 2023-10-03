using SensorM.Pages;

namespace SensorM
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(BluetoothDevicesPage), typeof(BluetoothDevicesPage));
        }
    }
}