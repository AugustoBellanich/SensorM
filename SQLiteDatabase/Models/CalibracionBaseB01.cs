using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteDatabase.Models
{
    [Table("CalibracionBaseB01")]
    public abstract class CalibracionBaseB01
    {
        // Id: Identificador único de la calibración.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Nombre: Nombre de la calibración.
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        // Descripcion: Descripción de la calibración. Tipo de suelo, etc.
        [MaxLength(500)]
        public string Descripcion { get; set; }

        // TipoCalibracion: Tipo de calibración.
        [Required]
        public TipoCalibracion TipoCalibracion { get; set; }

        // FactorCorreccionTemperatura: Factor de corrección por temperatura.
        public double? FactorCorreccionTemperatura { get; set; }

        // FactorCorreccionSalinidad: Factor de corrección por salinidad.
        public double? FactorCorreccionSalinidad { get; set; }

        // Colección de mediciones que utilizaron esta calibración
        public virtual ICollection<Medicion> Mediciones { get; set; } = new List<Medicion>();

        // CalcularHumedad: Método abstracto para calcular la humedad.
        public abstract double CalcularHumedad(double valorCapacitancia, double temperatura, double salinidad);
    }
    public enum TipoCalibracion
    {
        Exponencial,
        Lineal,
        Logaritmica,
        Polinomica,
        Potencia
        // Otros tipos de calibración que necesites
    }
}
