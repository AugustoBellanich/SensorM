using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDatabase.Models
{
    internal class CalibracionPotenciaB01: CalibracionBaseB01
    {
        // Formula: Humedad = CoeficienteA * (Capacitancia ^ ExponenteB)
        public double CoeficienteA { get; set; }
        public double ExponenteB { get; set; }

        public override double CalcularHumedad(double valorCapacitancia, double temperatura, double salinidad)
        {
            // Aplicar la fórmula de potencia y los factores de corrección
            double humedad = CoeficienteA * Math.Pow(valorCapacitancia, ExponenteB);
            // Aplicar correcciones si están definidas
            if (FactorCorreccionTemperatura.HasValue)
                humedad *= (1 + FactorCorreccionTemperatura.Value * (temperatura - 25)); // Asumiendo 25°C como temperatura de referencia
            if (FactorCorreccionSalinidad.HasValue)
                humedad *= (1 + FactorCorreccionSalinidad.Value * salinidad);
            return humedad;
        }
    }
}
