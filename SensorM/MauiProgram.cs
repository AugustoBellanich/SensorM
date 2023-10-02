using Microsoft.Extensions.Logging;
using SensorM.Interfaces;
using SensorM.Services;
using SensorM.ViewModels;
using SQLiteDatabase.Interfaces;
using SQLiteDatabase.Services;

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
                });

            // Registro de servicios de la biblioteca de clases
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();

            builder.Logging.AddDebug();

            builder.Services.AddSingleton<IThemeService, ThemeService>();
            builder.Services.AddTransient<InicioViewModel>();


            return builder.Build();
        }
    }
}