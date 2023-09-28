using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDatabase.Models
{
    public class Medicion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // IdDispositivo: Identificador del dispositivo que realizó la medición.
        public int IdDispositivo { get; set; }
        // FechaHora: Fecha y hora en que se realizó la medición.
        public DateTime FechaHora { get; set; }

        // Propiedad de navegación al dispositivo
        public virtual Dispositivo Dispositivo { get; set; }

        // Clave foránea de la calibración utilizada
        public int? IdCalibracionUtilizada { get; set; }

        // Propiedad de navegación a la calibración utilizada (si es necesario)
        public virtual CalibracionBaseB01 CalibracionUtilizada { get; set; }

    }
}
