using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteDatabase.Models
{
    [Table("Dispositivo")]
    public class Dispositivo
    {
        public Dispositivo()
        {
            // Inicializar propiedades con valores por defecto.
            FechaCreacion = DateTime.Now;
            FechaModificacion = DateTime.Now;
        }

        // Id: Identificador único del dispositivo.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Nombre: Nombre del dispositivo. (A01001, A01002, B01001, C01001, etc)
        [Required]
        [MaxLength(6)]
        public string Nombre { get; set; }

        // TipoDispositivo: Propiedades específicas de cada tipo de dispositivo (A01, A02, B01, C01, etc)
        [Required]
        public string TipoDispositivo { get; set; }

        // UsuarioPropietario: Usuario responsable del dispositivo.
        [Required]
        public string UsuarioPropietario { get; set; }

        // Estado del Dispositivo: Si los dispositivos pueden tener diferentes estados(por ejemplo, Activo, Inactivo, EnMantenimiento).
        public EstadoDispositivo Estado { get; set; }

        // Latitud: Latitud geográfica del dispositivo.
        public double? Latitud { get; set; }

        // Longitud: Longitud geográfica del dispositivo.
        public double? Longitud { get; set; }

        // FechaCreacion: Fecha y hora de creación del dispositivo.
        [Required]
        public DateTime FechaCreacion { get; set; }

        // FechaModificacion: Fecha y hora de la última modificación del dispositivo.
        [Required]
        public DateTime FechaModificacion { get; set; }

        // FechaUltimaConexion: Fecha y hora de la última conexión del dispositivo.
        public DateTime? FechaUltimaConexion { get; set; }

        // Colección de mediciones realizadas por este dispositivo
        public virtual ICollection<Medicion> Mediciones { get; set; } = new List<Medicion>();
    }

    // EstadoDispositivo: Enumeración de los posibles estados de un dispositivo.
    public enum EstadoDispositivo
    {
        Activo,
        Inactivo,
        EnMantenimiento,
        // Otros estados que necesites
    }

    // TipoDispositivo: Enumeración de los posibles tipos de dispositivo.
    public enum TipoDispositivo
    {
        A01,
        A02,
        B01
        //C01,
        // Otros tipos de dispositivo que necesites
    }
}
