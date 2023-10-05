using SensorM.ViewModels;
using UraniumUI.Pages;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;

namespace SensorM.Pages;

public partial class SensorB01Page : UraniumContentPage
{
    private readonly SensorB01ViewModel _viewModel;
    public SensorB01Page(SensorB01ViewModel viewModel)
    {         
        InitializeComponent();
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        BindingContext = _viewModel;

    }
}