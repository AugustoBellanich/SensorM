using Microsoft.Extensions.Logging;
using SensorM.Interfaces;
using SensorM.Services;
using SensorM.ViewModels;
using SQLiteDatabase.Interfaces;
using SQLiteDatabase.Services;
using UraniumUI;

namespace SensorM
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialIconFonts();
                });

            builder.UseUraniumUI();
            builder.UseUraniumUIMaterial();

            // Registro de servicios de la biblioteca de clases
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();

            #if __ANDROID__
            // Registro de IBluetoothService para Android
            builder.Services.AddTransient<IBluetoothService, SensorM.Platforms.Android.AndroidBluetoothService>();
            #endif

            // Registro de INavigationService
            builder.Services.AddTransient<INavigationService>(provider =>
            {
                var mainPage = provider.GetRequiredService<MainPage>();
                var bluetoothService = provider.GetRequiredService<IBluetoothService>();
                return new NavigationService(mainPage, bluetoothService);
            });


            builder.Services.AddSingleton<IThemeService, ThemeService>();
            builder.Services.AddTransient<InicioViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Logging.AddDebug();

            return builder.Build();
        }
    }
}