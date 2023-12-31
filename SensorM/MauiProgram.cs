﻿using Microsoft.Extensions.Logging;
using SensorM.Interfaces;
using SensorM.Pages;
using SensorM.Services;
using SensorM.ViewModels;
using SQLiteDatabase.Interfaces;
using SQLiteDatabase.Services;
using UraniumUI;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace SensorM
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseSkiaSharp(true)
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialIconFonts();
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // Registro de INavigationService
            builder.Services.AddSingleton<INavigationService, NavigationService>();


            builder.UseUraniumUI();
            //builder.UseUraniumUIMaterial();

            // Registro de servicios de la biblioteca de clases
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();



#if __ANDROID__

            builder.Services.AddSingleton<IBluetoothService, SensorM.Platforms.Android.AndroidBluetoothService>();

#endif

            //#if WINDOWS
            //// Registro de IBluetoothService para windows
            //builder.Services.AddTransient<IBluetoothService, SensorM.Platforms.Windows.WindowsBluetoothService>();
            //#endif


            // Registro de IThemeService
            builder.Services.AddSingleton<IThemeService, ThemeService>();

            //Registro de ViewModel
            builder.Services.AddSingleton<InicioViewModel>();
            builder.Services.AddTransient<BluetoothDevicesViewModel>();  // Transient para que se cree una nueva instancia cada vez que se solicite
            builder.Services.AddSingleton<SensorB01ViewModel>();
            

            // Registro de Page
            builder.Services.AddSingleton<InicioPage>();
            builder.Services.AddTransient<BluetoothDevicesPage>();
            builder.Services.AddSingleton<SensorB01Page>();


            return builder.Build();
        }
    }
}