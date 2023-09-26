using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorM.Models
{
    internal class DispositivoB01: Dispositivo
    {
        // EstadoGuardando: Indica si el dispositivo está actualmente guardando valores en su memoria.
        public bool EstadoGuardando { get; set; }

        // EstadoModuloSD: Indica si el módulo SD está conectado y funcionando correctamente.
        public bool EstadoModuloSD { get; set; }

        // PorcentajeMemoriaLibre: Indica el porcentaje de memoria libre en el dispositivo.
        public int PorcentajeMemoriaLibre { get; set; }

        // PorcentajeMemoriaLibre: Indica el porcentaje de memoria libre en el dispositivo.
        public int PorcentajeBateria { get; set; }

        // CalibracionesSeleccionadas: Indica las calibraciones seleccionadas para cada sensor.
        public Dictionary<string, string> CalibracionesSeleccionadas { get; set; } // <IdSensor, IdCalibracion>

    }
}
