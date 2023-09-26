using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorM.Models
{
    internal class CalibracionExponencialB01 : CalibracionBaseB01
    {
        // Formula= CoeficienteInicial * (Base ^ (CoeficienteExponente * Capacitancia)) + TerminoConstante

        public double CoeficienteInicial { get; set; } // 'a' en la fórmula
        public double Base { get; set; } // 'b' en la fórmula, base de la exponencial (en este caso, e)
        public double CoeficienteExponente { get; set; } // 'c' en la fórmula
        public double TerminoConstante { get; set; } // 'd' en la fórmula

        public override double CalcularHumedad(double valorCapacitancia, double temperatura, double salinidad)
        {
            // Aplicar la fórmula exponencial y los factores de corrección
            double humedad = CoeficienteInicial * Math.Exp(CoeficienteExponente * valorCapacitancia) + TerminoConstante;
            // Aplicar correcciones si están definidas
            if (FactorCorreccionTemperatura.HasValue)
                humedad *= (1 + FactorCorreccionTemperatura.Value * (temperatura - 25)); // Asumiendo 25°C como temperatura de referencia
            if (FactorCorreccionSalinidad.HasValue)
                humedad *= (1 + FactorCorreccionSalinidad.Value * salinidad);
            return humedad;
        }
    }
}
