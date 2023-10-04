using SensorM.Interfaces;
using SensorM.Pages;
using SensorM.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Application = Microsoft.Maui.Controls.Application;

namespace SensorM
{
    public partial class App : Application
    {
        public App()
        {
            MainPage = new AppShell();  // Establece la página principal de la aplicación
        }


        protected override void OnStart()
        {
            base.OnStart();

            // Establece los valores iniciales de los recursos dinámicos
            if (Resources.TryGetValue("LightBackgroundColor", out var lightBackgroundColor))
            {
                Resources["CurrentBackgroundColor"] = lightBackgroundColor;
            }

            if (Resources.TryGetValue("LightTextColor", out var lightTextColor))
            {
                Resources["CurrentTextColor"] = lightTextColor;
            }
        }
    }
}