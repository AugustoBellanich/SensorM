using Android.App;
using Android.Runtime;
using SensorM.Interfaces;
using SensorM.Platforms.Android;

namespace SensorM
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();


        // Registrar el Servicio de Bluetooth
        public override void OnCreate()
        {
            base.OnCreate();

            DependencyService.Register<IBluetoothService, AndroidBluetoothService>(); // Agregar esta línea para registrar la implementación en el contenedor de dependencias
        }
    }
}