using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorM.Models
{
    internal abstract class CalibracionBaseB01
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        // Descripcion: Descripción de la calibración. Tipo de suelo, etc.
        public string Descripcion { get; set; }
        public double? FactorCorreccionTemperatura { get; set; }
        public double? FactorCorreccionSalinidad { get; set; }

        public abstract double CalcularHumedad(double valorCapacitancia, double temperatura, double salinidad);
    }
}
