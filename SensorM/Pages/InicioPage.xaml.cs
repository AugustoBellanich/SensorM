using Microsoft.Maui.Controls;
using SensorM.ViewModels;
using SensorM.Services;
using SensorM.Interfaces;
using SQLiteDatabase.Models;
using UraniumUI.Pages;

namespace SensorM.Pages
{
    public partial class InicioPage : UraniumContentPage
    {
        private readonly IThemeService _themeService;
        private readonly InicioViewModel _viewModel;
        private readonly INavigationService _navigationService; // Inyectado para gestionar la navegación


        public InicioPage(IThemeService themeService, InicioViewModel viewModel, INavigationService navigationService)
        {
            // Asegurándonos de que las dependencias se inyectaron correctamente
            _themeService = themeService ?? throw new ArgumentNullException(nameof(themeService));
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            InitializeComponent();

            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (_viewModel as InicioViewModel)?.UpdateBluetoothStatus();
        }

    }
}