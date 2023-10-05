using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorM.Interfaces
{
    public interface IBluetoothService
    {
        // Buscar dispositivos, devuelve una lista de los dispositivos encontrados
        Task<IEnumerable<string>> BuscarDispositivosAsync();

        // Conectar a un dispositivo, devuelve true si se conecto correctamente
        Task<bool> ConectarAsync(string deviceName);

        // Enviar datos al dispositivo
        Task EnviarDatosAsync(string data);

        // Recibir datos del dispositivo
        Task<string> RecibirDatosAsync();

        Task DesconectarAsync();  // Desconectar del dispositivo




        bool IsConnected { get; }  // Propiedad para verificar si el dispositivo está conectado
        string ConnectedDeviceName { get; }  // Propiedad para obtener el nombre del dispositivo conectado


    }

}
