using Microsoft.Extensions.Logging;
using SensorM.Interface;
using SensorM.Services;
using SensorM.ViewModels;

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
            
#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IThemeService, ThemeService>();
            builder.Services.AddTransient<InicioViewModel>();


            return builder.Build();
        }
    }
}