using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDatabase.Models
{
    internal class MedicionB01:Medicion
    {
        // Capacitancia1: Valor de la primera capacitancia.
        public double Capacitancia1 { get; set; }

        // Capacitancia2: Valor de la segunda capacitancia.
        public double Capacitancia2 { get; set; }

        // Capacitancia3: Valor de la tercera capacitancia.
        public double Capacitancia3 { get; set; }

        // Temperatura: Valor de la temperatura.
        public double Temperatura { get; set; }

        // Humedad: Valor de la humedad.
        public double? HumedadCalculadaSensor1 { get; set; }
        public double? HumedadCalculadaSensor2 { get; set; }
        public double? HumedadCalculadaSensor3 { get; set; }
    }
}
