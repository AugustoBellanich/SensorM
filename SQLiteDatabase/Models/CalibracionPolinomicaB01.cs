using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDatabase.Models
{
    internal class CalibracionPolinomicaB01:CalibracionBaseB01
    {
        //Formula: Humedad = CoeficienteA * Capacitancia^2 + CoeficienteB * Capacitancia + CoeficienteC
        public double CoeficienteA { get; set; } // Coeficiente del término cuadrático
        public double CoeficienteB { get; set; } // Coeficiente del término lineal
        public double CoeficienteC { get; set; } // Término constante

        public override double CalcularHumedad(double valorCapacitancia, double temperatura, double salinidad)
        {
            // Aplicar la fórmula polinómica y los factores de corrección
            double humedad = CoeficienteA * Math.Pow(valorCapacitancia, 2) + CoeficienteB * valorCapacitancia + CoeficienteC;
            // Aplicar correcciones si están definidas
            if (FactorCorreccionTemperatura.HasValue)
                humedad *= (1 + FactorCorreccionTemperatura.Value * (temperatura - 25)); // Asumiendo 25°C como temperatura de referencia
            if (FactorCorreccionSalinidad.HasValue)
                humedad *= (1 + FactorCorreccionSalinidad.Value * salinidad);
            return humedad;
        }
    }
}
