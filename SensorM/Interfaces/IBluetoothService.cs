using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorM.Interfaces
{
    public interface IBluetoothService
    {
        Task<IEnumerable<DispositivoBluetooth>> BuscarDispositivosAsync();
        Task ConectarAsync(DispositivoBluetooth dispositivo);
        Task EnviarDatosAsync(byte[] datos);
        Task<byte[]> RecibirDatosAsync();
    }

}
