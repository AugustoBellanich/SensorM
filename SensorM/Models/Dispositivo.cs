using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorM.Models
{
    public class Dispositivo
    {
        // Id: Identificador único del dispositivo.
        public string Id { get; set; }

        // Nombre: Nombre del dispositivo. (A01001, A01002, B01001, C01001, etc)
        public string Nombre { get; set; }

        // Descripcion: Descripción del dispositivo (A01, A02, B01, C01, etc)
        public string Descripcion { get; set; }

        // Estado: Indica si el dispositivo está conectado o no.
        public bool EstadoModuloReloj { get; set; }

        // Latitud: Latitud geográfica del dispositivo.
        public double? Latitud { get; set; }

        // Longitud: Longitud geográfica del dispositivo.
        public double? Longitud { get; set; }

        // FechaUltimaConexion: Fecha y hora de la última conexión del dispositivo.
        public DateTime? FechaUltimaConexion { get; set; }

    }
}
