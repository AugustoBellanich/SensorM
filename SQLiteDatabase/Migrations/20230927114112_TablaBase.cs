using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLiteDatabase.Migrations
{
    /// <inheritdoc />
    public partial class TablaBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalibracionBaseB01",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    TipoCalibracion = table.Column<int>(type: "INTEGER", nullable: false),
                    FactorCorreccionTemperatura = table.Column<double>(type: "REAL", nullable: true),
                    FactorCorreccionSalinidad = table.Column<double>(type: "REAL", nullable: true),
                    CoeficienteInicial = table.Column<double>(type: "REAL", nullable: true),
                    Base = table.Column<double>(type: "REAL", nullable: true),
                    CoeficienteExponente = table.Column<double>(type: "REAL", nullable: true),
                    TerminoConstante = table.Column<double>(type: "REAL", nullable: true),
                    Pendiente = table.Column<double>(type: "REAL", nullable: true),
                    OrdenadaOrigen = table.Column<double>(type: "REAL", nullable: true),
                    CoeficienteLogaritmico = table.Column<double>(type: "REAL", nullable: true),
                    BaseLogaritmica = table.Column<double>(type: "REAL", nullable: true),
                    CalibracionLogaritmicaB01_TerminoConstante = table.Column<double>(type: "REAL", nullable: true),
                    CoeficienteA = table.Column<double>(type: "REAL", nullable: true),
                    CoeficienteB = table.Column<double>(type: "REAL", nullable: true),
                    CoeficienteC = table.Column<double>(type: "REAL", nullable: true),
                    CalibracionPotenciaB01_CoeficienteA = table.Column<double>(type: "REAL", nullable: true),
                    ExponenteB = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibracionBaseB01", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dispositivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    TipoDispositivo = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioPropietario = table.Column<string>(type: "TEXT", nullable: false),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    Latitud = table.Column<double>(type: "REAL", nullable: true),
                    Longitud = table.Column<double>(type: "REAL", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaUltimaConexion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PropiedadA = table.Column<string>(type: "TEXT", nullable: true),
                    PropiedadB = table.Column<string>(type: "TEXT", nullable: true),
                    PropiedadC = table.Column<string>(type: "TEXT", nullable: true),
                    PropiedadD = table.Column<string>(type: "TEXT", nullable: true),
                    EstadoGuardando = table.Column<bool>(type: "INTEGER", nullable: true),
                    EstadoModuloReloj = table.Column<bool>(type: "INTEGER", nullable: true),
                    EstadoModuloSD = table.Column<bool>(type: "INTEGER", nullable: true),
                    PorcentajeMemoriaLibre = table.Column<int>(type: "INTEGER", nullable: true),
                    PorcentajeBateria = table.Column<int>(type: "INTEGER", nullable: true),
                    IdCalibracionSensor1 = table.Column<string>(type: "TEXT", nullable: true),
                    IdCalibracionSensor2 = table.Column<string>(type: "TEXT", nullable: true),
                    IdCalibracionSensor3 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispositivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mediciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdDispositivo = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IdCalibracionUtilizada = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mediciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mediciones_CalibracionBaseB01_IdCalibracionUtilizada",
                        column: x => x.IdCalibracionUtilizada,
                        principalTable: "CalibracionBaseB01",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mediciones_Dispositivo_IdDispositivo",
                        column: x => x.IdDispositivo,
                        principalTable: "Dispositivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mediciones_IdCalibracionUtilizada",
                table: "Mediciones",
                column: "IdCalibracionUtilizada");

            migrationBuilder.CreateIndex(
                name: "IX_Mediciones_IdDispositivo",
                table: "Mediciones",
                column: "IdDispositivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mediciones");

            migrationBuilder.DropTable(
                name: "CalibracionBaseB01");

            migrationBuilder.DropTable(
                name: "Dispositivo");
        }
    }
}
