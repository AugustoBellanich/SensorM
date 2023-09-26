using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorM.Models
{
    internal class Medicion
    {
        // IdDispositivo: Identificador del dispositivo que realizó la medición.
        public string IdDispositivo { get; set; }
        // FechaHora: Fecha y hora en que se realizó la medición.
        public DateTime FechaHora { get; set; }

    }
}
