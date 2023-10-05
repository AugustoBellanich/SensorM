using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorM.Events
{
    public class BluetoothConnectionEventArgs : EventArgs
    {        
        public bool IsConnected { get; set; }
        public string DeviceName { get; set; }
    }
}
