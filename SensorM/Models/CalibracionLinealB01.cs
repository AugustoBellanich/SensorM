using SensorM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorM.Models
{
    internal class CalibracionLinealB01 : CalibracionBaseB01
    {
        // Formula: Humedad = Pendiente * Capacitancia + OrdenadaOrigen
        public double Pendiente { get; set; }
        public double OrdenadaOrigen { get; set; }

        public override double CalcularHumedad(double valorCapacitancia, double temperatura, double salinidad)
        {
            // Aplicar la fórmula lineal y los factores de corrección
            double humedad = Pendiente * valorCapacitancia + OrdenadaOrigen;
            // Aplicar correcciones si están definidas
            if (FactorCorreccionTemperatura.HasValue)
                humedad *= (1 + FactorCorreccionTemperatura.Value * (temperatura - 25)); // Asumiendo 25°C como temperatura de referencia
            if (FactorCorreccionSalinidad.HasValue)
                humedad *= (1 + FactorCorreccionSalinidad.Value * salinidad);
            return humedad;
        }
    }
}
