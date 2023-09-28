using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDatabase.Models
{
    public class DispositivoB01: Dispositivo
    {
        // EstadoGuardando: Indica si el dispositivo está actualmente guardando valores en su memoria.
        public bool EstadoGuardando { get; set; }

        // EstadoModuloReloj: Indica el modulo reloj del dispositivo está funcnionando o no.
        public bool EstadoModuloReloj { get; set; }

        // EstadoModuloSD: Indica si el módulo SD está conectado y funcionando correctamente.
        public bool EstadoModuloSD { get; set; }

        // PorcentajeMemoriaLibre: Indica el porcentaje de memoria libre en el dispositivo.
        public int PorcentajeMemoriaLibre { get; set; }

        // PorcentajeMemoriaLibre: Indica el porcentaje de memoria libre en el dispositivo.
        public int PorcentajeBateria { get; set; }

        // IdCalibracionSensor1: ID de la calibración seleccionada para el sensor capacitivo 1.
        public string IdCalibracionSensor1 { get; set; }

        // IdCalibracionSensor2: ID de la calibración seleccionada para el sensor capacitivo 2.
        public string IdCalibracionSensor2 { get; set; }

        // IdCalibracionSensor3: ID de la calibración seleccionada para el sensor capacitivo 3.
        public string IdCalibracionSensor3 { get; set; }

    }
}
