using Microsoft.EntityFrameworkCore;
using SQLiteDatabase.Models;

namespace SQLiteDatabase.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Medicion> Mediciones { get; set; }
        public DbSet<CalibracionBaseB01> Calibraciones { get; set; }

        // Agrega otros DbSet para otras entidades

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configura la conexión a la base de datos SQLite
            optionsBuilder.UseSqlite("Filename=AppData.db");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalibracionBaseB01>()
                .HasDiscriminator<TipoCalibracion>("TipoCalibracion")
                .HasValue<CalibracionExponencialB01>(TipoCalibracion.Exponencial)
                .HasValue<CalibracionLinealB01>(TipoCalibracion.Lineal)
                .HasValue<CalibracionLogaritmicaB01>(TipoCalibracion.Logaritmica)
                .HasValue<CalibracionPolinomicaB01>(TipoCalibracion.Polinomica)
                .HasValue<CalibracionPotenciaB01>(TipoCalibracion.Potencia);

            modelBuilder.Entity<Dispositivo>()
                .HasDiscriminator<string>("TipoDispositivo")
                .HasValue<DispositivoA01>("A01")
                .HasValue<DispositivoA02>("A02")
                .HasValue<DispositivoB01>("B01");

            modelBuilder.Entity<Medicion>()
                .HasOne(m => m.Dispositivo)
                .WithMany() // Si el dispositivo tiene una colección de mediciones, reemplaza con .WithMany(d => d.Mediciones)
                .HasForeignKey(m => m.IdDispositivo);

            modelBuilder.Entity<Medicion>()
                .HasOne(m => m.CalibracionUtilizada)
                .WithMany() // Si la calibración tiene una colección de mediciones, reemplaza con .WithMany(c => c.Mediciones)
                .HasForeignKey(m => m.IdCalibracionUtilizada);

            modelBuilder.Entity<Medicion>()
                .HasOne(m => m.Dispositivo)
                .WithMany(d => d.Mediciones)
                .HasForeignKey(m => m.IdDispositivo);

            modelBuilder.Entity<Medicion>()
                .HasOne(m => m.CalibracionUtilizada)
                .WithMany(c => c.Mediciones)
                .HasForeignKey(m => m.IdCalibracionUtilizada);

            // Configuraciones adicionales...
        }
    }
}

