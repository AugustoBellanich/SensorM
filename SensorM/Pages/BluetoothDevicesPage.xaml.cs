using Microsoft.Maui.Controls;
using SensorM.ViewModels;
using System;
using UraniumUI.Pages;

namespace SensorM.Pages
{
    public partial class BluetoothDevicesPage : UraniumContentPage
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
