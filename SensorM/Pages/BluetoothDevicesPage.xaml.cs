using Microsoft.Maui.Controls;
using SensorM.ViewModels;
using System;

namespace SensorM.Pages
{
    public partial class BluetoothDevicesPage : ContentPage
    {
        private readonly BluetoothDevicesViewModel _viewModel;

        public BluetoothDevicesPage(BluetoothDevicesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            BindingContext = _viewModel;
        }
    }
}
