using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorM.Data
{
    public class SensorB01Data
    {
        public double Capacitancia1 { get; set; }
        public double Capacitancia2 { get; set; }
        public double Capacitancia3 { get; set; }
        public double Temperatura { get; set; }
        public double Humedad1 { get; set; }
        public double Humedad2 { get; set; }
        public double Humedad3 { get; set; }

        public void CalcularHumedades()
        {
            Humedad1 = CalcularHumedad(Capacitancia1);
            Humedad2 = CalcularHumedad(Capacitancia2);
            Humedad3 = CalcularHumedad(Capacitancia3);
        }

        private double CalcularHumedad(double capacitancia)
        {
            double h = 2408.565 - (15.812 * capacitancia) + (0.0348 * Math.Pow(capacitancia, 2)) - (2.56e-5 * Math.Pow(capacitancia, 3));
            return Math.Clamp(h, 0, 100); // Asegura que la humedad esté en el rango [0, 100]
        }
    }

}
