using Microsoft.Maui.Controls;
using SensorM.ViewModels;
using SensorM.Services;
using SensorM.Interface;
using SensorM.Models;

namespace SensorM.Pages
{
    public partial class InicioPage : ContentPage
    {
        private readonly IThemeService _themeService;
        private readonly InicioViewModel _viewModel;

        public InicioPage(IThemeService themeService, InicioViewModel viewModel)
        {
            // Asegurándonos de que las dependencias se inyectaron correctamente
            _themeService = themeService ?? throw new ArgumentNullException(nameof(themeService));
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            InitializeComponent();

            BindingContext = _viewModel;
        }

        private void OnThemeToggled(object sender, ToggledEventArgs e)
        {
            _themeService.SetTheme(e.Value ? Theme.Dark : Theme.Light);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica si hay algún elemento seleccionado
            if (e.CurrentSelection.Count > 0)
            {
                // Obtén el primer elemento seleccionado y verifica si es un Dispositivo
                if (e.CurrentSelection[0] is Dispositivo dispositivoSeleccionado)
                {
                    // Haz algo con el dispositivo seleccionado
                    // Por ejemplo, navegar a una nueva página que muestra detalles sobre el dispositivo
                }
            }
        }
    }
}