﻿using SensorM.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SensorM.ViewModels
{
    public partial class BluetoothDevicesViewModel : ObservableObject
    {
        private readonly IBluetoothService _bluetoothService;
        private ObservableCollection<string> _dispositivos;
        private string _selectedDevice;
        private string _statusMessage;
        private bool _isBusy;

        public BluetoothDevicesViewModel(IBluetoothService bluetoothService)
        {
            _bluetoothService = bluetoothService ?? throw new ArgumentNullException(nameof(bluetoothService));
        }

        public ObservableCollection<string> Dispositivos
        {
            get => _dispositivos;
            set => SetProperty(ref _dispositivos, value);
        }

        public string SelectedDevice
        {
            get => _selectedDevice;
            set => SetProperty(ref _selectedDevice, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        [RelayCommand]
        public async Task BuscarDispositivos()
        {
            try
            {
                IsBusy = true;
                var dispositivos = await _bluetoothService.BuscarDispositivosAsync();
                Dispositivos = new ObservableCollection<string>(dispositivos);
                StatusMessage = Dispositivos.Count > 0 ? "Dispositivos encontrados" : "No se encontraron dispositivos";
            }
            catch (Exception ex)
            {
                StatusMessage = "Error al buscar dispositivos: " + ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task Conectar()
        {
            if (string.IsNullOrEmpty(SelectedDevice))
            {
                StatusMessage = "Por favor, selecciona un dispositivo para conectarte.";
                return;
            }

            try
            {
                IsBusy = true;
                bool isConnected = await _bluetoothService.ConectarAsync(SelectedDevice);
                StatusMessage = isConnected ? "Conectado a " + SelectedDevice : "No se pudo conectar a " + SelectedDevice;
            }
            catch (Exception ex)
            {
                StatusMessage = "Error al intentar conectar: " + ex.Message;
            }
            finally
            {
                IsBusy = false;

                if (_bluetoothService.IsConnected)
                {
                    await Shell.Current.Navigation.PopAsync();
                }
            }
        }
    }
}
