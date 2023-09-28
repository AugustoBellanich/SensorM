using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDatabase.Models
{
    internal class CalibracionLogaritmicaB01: CalibracionBaseB01
    {
        // Formula: Humedad = CoeficienteLogaritmico * Log(Capacitancia, BaseLogaritmica) + TerminoConstante
        public double CoeficienteLogaritmico { get; set; } // 'a' en la fórmula
        public double BaseLogaritmica { get; set; } // 'b' en la fórmula
        public double TerminoConstante { get; set; } // 'c' en la fórmula

        public override double CalcularHumedad(double valorCapacitancia, double temperatura, double salinidad)
        {
            // Aplicar la fórmula logarítmica y los factores de corrección
            double humedad = CoeficienteLogaritmico * Math.Log(valorCapacitancia, BaseLogaritmica) + TerminoConstante;
            // Aplicar correcciones si están definidas
            if (FactorCorreccionTemperatura.HasValue)
                humedad *= (1 + FactorCorreccionTemperatura.Value * (temperatura - 25)); // Asumiendo 25°C como temperatura de referencia
            if (FactorCorreccionSalinidad.HasValue)
                humedad *= (1 + FactorCorreccionSalinidad.Value * salinidad);
            return humedad;
        }
    }
}
