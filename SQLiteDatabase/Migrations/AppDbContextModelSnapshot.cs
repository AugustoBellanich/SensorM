﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SQLiteDatabase.Data;

#nullable disable

namespace SQLiteDatabase.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("SQLiteDatabase.Models.CalibracionBaseB01", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<double?>("FactorCorreccionSalinidad")
                        .HasColumnType("REAL");

                    b.Property<double?>("FactorCorreccionTemperatura")
                        .HasColumnType("REAL");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("TipoCalibracion")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CalibracionBaseB01");

                    b.HasDiscriminator<int>("TipoCalibracion");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SQLiteDatabase.Models.Dispositivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Estado")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaUltimaConexion")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Latitud")
                        .HasColumnType("REAL");

                    b.Property<double?>("Longitud")
                        .HasColumnType("REAL");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoDispositivo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UsuarioPropietario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Dispositivo");

                    b.HasDiscriminator<string>("TipoDispositivo").HasValue("Dispositivo");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SQLiteDatabase.Models.Medicion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("TEXT");

                    b.Property<int?>("IdCalibracionUtilizada")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdDispositivo")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IdCalibracionUtilizada");

                    b.HasIndex("IdDispositivo");

                    b.ToTable("Mediciones");
                });

            modelBuilder.Entity("SQLiteDatabase.Models.CalibracionExponencialB01", b =>
                {
                    b.HasBaseType("SQLiteDatabase.Models.CalibracionBaseB01");

                    b.Property<double>("Base")
                        .HasColumnType("REAL");

                    b.Property<double>("CoeficienteExponente")
                        .HasColumnType("REAL");

                    b.Property<double>("CoeficienteInicial")
                        .HasColumnType("REAL");

                    b.Property<double>("TerminoConstante")
                        .HasColumnType("REAL");

                    b.ToTable("CalibracionBaseB01");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("SQLiteDatabase.Models.CalibracionLinealB01", b =>
                {
                    b.HasBaseType("SQLiteDatabase.Models.CalibracionBaseB01");

                    b.Property<double>("OrdenadaOrigen")
                        .HasColumnType("REAL");

                    b.Property<double>("Pendiente")
                        .HasColumnType("REAL");

                    b.ToTable("CalibracionBaseB01");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("SQLiteDatabase.Models.CalibracionLogaritmicaB01", b =>
                {
                    b.HasBaseType("SQLiteDatabase.Models.CalibracionBaseB01");

                    b.Property<double>("BaseLogaritmica")
                        .HasColumnType("REAL");

                    b.Property<double>("CoeficienteLogaritmico")
                        .HasColumnType("REAL");

                    b.Property<double>("TerminoConstante")
                        .HasColumnType("REAL");

                    b.ToTable("CalibracionBaseB01", t =>
                        {
                            t.Property("TerminoConstante")
                                .HasColumnName("CalibracionLogaritmicaB01_TerminoConstante");
                        });

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("SQLiteDatabase.Models.CalibracionPolinomicaB01", b =>
                {
                    b.HasBaseType("SQLiteDatabase.Models.CalibracionBaseB01");

                    b.Property<double>("CoeficienteA")
                        .HasColumnType("REAL");

                    b.Property<double>("CoeficienteB")
                        .HasColumnType("REAL");

                    b.Property<double>("CoeficienteC")
                        .HasColumnType("REAL");

                    b.ToTable("CalibracionBaseB01");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("SQLiteDatabase.Models.CalibracionPotenciaB01", b =>
                {
                    b.HasBaseType("SQLiteDatabase.Models.CalibracionBaseB01");

                    b.Property<double>("CoeficienteA")
                        .HasColumnType("REAL");

                    b.Property<double>("ExponenteB")
                        .HasColumnType("REAL");

                    b.ToTable("CalibracionBaseB01", t =>
                        {
                            t.Property("CoeficienteA")
                                .HasColumnName("CalibracionPotenciaB01_CoeficienteA");
                        });

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("SQLiteDatabase.Models.DispositivoA01", b =>
                {
                    b.HasBaseType("SQLiteDatabase.Models.Dispositivo");

                    b.Property<string>("PropiedadA")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PropiedadB")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("Dispositivo");

                    b.HasDiscriminator().HasValue("A01");
                });

            modelBuilder.Entity("SQLiteDatabase.Models.DispositivoA02", b =>
                {
                    b.HasBaseType("SQLiteDatabase.Models.Dispositivo");

                    b.Property<string>("PropiedadC")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PropiedadD")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("Dispositivo");

                    b.HasDiscriminator().HasValue("A02");
                });

            modelBuilder.Entity("SQLiteDatabase.Models.DispositivoB01", b =>
                {
                    b.HasBaseType("SQLiteDatabase.Models.Dispositivo");

                    b.Property<bool>("EstadoGuardando")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EstadoModuloReloj")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EstadoModuloSD")
                        .HasColumnType("INTEGER");

                    b.Property<string>("IdCalibracionSensor1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IdCalibracionSensor2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IdCalibracionSensor3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PorcentajeBateria")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PorcentajeMemoriaLibre")
                        .HasColumnType("INTEGER");

                    b.ToTable("Dispositivo");

                    b.HasDiscriminator().HasValue("B01");
                });

            modelBuilder.Entity("SQLiteDatabase.Models.Medicion", b =>
                {
                    b.HasOne("SQLiteDatabase.Models.CalibracionBaseB01", "CalibracionUtilizada")
                        .WithMany("Mediciones")
                        .HasForeignKey("IdCalibracionUtilizada")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SQLiteDatabase.Models.Dispositivo", "Dispositivo")
                        .WithMany("Mediciones")
                        .HasForeignKey("IdDispositivo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CalibracionUtilizada");

                    b.Navigation("Dispositivo");
                });

            modelBuilder.Entity("SQLiteDatabase.Models.CalibracionBaseB01", b =>
                {
                    b.Navigation("Mediciones");
                });

            modelBuilder.Entity("SQLiteDatabase.Models.Dispositivo", b =>
                {
                    b.Navigation("Mediciones");
                });
#pragma warning restore 612, 618
        }
    }
}