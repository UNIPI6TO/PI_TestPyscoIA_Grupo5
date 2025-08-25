using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Instrucciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdPais = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provincias_Paises_IdPais",
                        column: x => x.IdPais,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionesTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TiempoExpiracion = table.Column<int>(type: "int", nullable: false),
                    IdTipoTest = table.Column<int>(type: "int", nullable: false),
                    IdEvaluador = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionesTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracionesTest_Evaluadores_IdEvaluador",
                        column: x => x.IdEvaluador,
                        principalTable: "Evaluadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfiguracionesTest_TipoTest_IdTipoTest",
                        column: x => x.IdTipoTest,
                        principalTable: "TipoTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoSecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Instrucciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoTest = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoSecciones_TipoTest_IdTipoTest",
                        column: x => x.IdTipoTest,
                        principalTable: "TipoTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdProvincia = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciudades_Provincias_IdProvincia",
                        column: x => x.IdProvincia,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionesSecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroPreguntas = table.Column<int>(type: "int", nullable: false),
                    IdConfiguracionesTest = table.Column<int>(type: "int", nullable: false),
                    IdTipoSecciones = table.Column<int>(type: "int", nullable: false),
                    FormulaAgregado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionesSecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracionesSecciones_ConfiguracionesTest_IdConfiguracionesTest",
                        column: x => x.IdConfiguracionesTest,
                        principalTable: "ConfiguracionesTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfiguracionesSecciones_TipoSecciones_IdTipoSecciones",
                        column: x => x.IdTipoSecciones,
                        principalTable: "TipoSecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCiudad = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_Ciudades_IdCiudad",
                        column: x => x.IdCiudad,
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionesPreguntas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdConfiguracionSecciones = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionesPreguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracionesPreguntas_ConfiguracionesSecciones_IdConfiguracionSecciones",
                        column: x => x.IdConfiguracionSecciones,
                        principalTable: "ConfiguracionesSecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantidadPreguntas = table.Column<int>(type: "int", nullable: false),
                    IdConfiguracionTest = table.Column<int>(type: "int", nullable: false),
                    IdEvaluador = table.Column<int>(type: "int", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_ConfiguracionesTest_IdConfiguracionTest",
                        column: x => x.IdConfiguracionTest,
                        principalTable: "ConfiguracionesTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Test_Evaluadores_IdEvaluador",
                        column: x => x.IdEvaluador,
                        principalTable: "Evaluadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Test_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionesOpciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Opcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(8,4)", precision: 8, scale: 4, nullable: false),
                    IdConfiguracionBancoPreguntas = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionesOpciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracionesOpciones_ConfiguracionesPreguntas_IdConfiguracionBancoPreguntas",
                        column: x => x.IdConfiguracionBancoPreguntas,
                        principalTable: "ConfiguracionesPreguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestSecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<float>(type: "real", nullable: true),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicioTest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinTest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duracion = table.Column<int>(type: "int", nullable: true),
                    Contestadas = table.Column<int>(type: "int", nullable: true),
                    NoContestadas = table.Column<int>(type: "int", nullable: true),
                    CantidadPreguntas = table.Column<int>(type: "int", nullable: true),
                    Completado = table.Column<bool>(type: "bit", nullable: true),
                    Iniciado = table.Column<bool>(type: "bit", nullable: true),
                    IdTest = table.Column<int>(type: "int", nullable: false),
                    IdConfiguracionSecciones = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSecciones_ConfiguracionesSecciones_IdConfiguracionSecciones",
                        column: x => x.IdConfiguracionSecciones,
                        principalTable: "ConfiguracionesSecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestSecciones_Test_IdTest",
                        column: x => x.IdTest,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestsPreguntas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(8,4)", precision: 8, scale: 4, nullable: false),
                    IdConfiguracionBancoPreguntas = table.Column<int>(type: "int", nullable: false),
                    IdTestSecciones = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestsPreguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestsPreguntas_ConfiguracionesPreguntas_IdConfiguracionBancoPreguntas",
                        column: x => x.IdConfiguracionBancoPreguntas,
                        principalTable: "ConfiguracionesPreguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestsPreguntas_TestSecciones_IdTestSecciones",
                        column: x => x.IdTestSecciones,
                        principalTable: "TestSecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_IdProvincia",
                table: "Ciudades",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionesOpciones_IdConfiguracionBancoPreguntas",
                table: "ConfiguracionesOpciones",
                column: "IdConfiguracionBancoPreguntas");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionesPreguntas_IdConfiguracionSecciones",
                table: "ConfiguracionesPreguntas",
                column: "IdConfiguracionSecciones");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionesSecciones_IdConfiguracionesTest",
                table: "ConfiguracionesSecciones",
                column: "IdConfiguracionesTest");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionesSecciones_IdTipoSecciones",
                table: "ConfiguracionesSecciones",
                column: "IdTipoSecciones");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionesTest_IdEvaluador",
                table: "ConfiguracionesTest",
                column: "IdEvaluador");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionesTest_IdTipoTest",
                table: "ConfiguracionesTest",
                column: "IdTipoTest");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_IdCiudad",
                table: "Pacientes",
                column: "IdCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_Provincias_IdPais",
                table: "Provincias",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_Test_IdConfiguracionTest",
                table: "Test",
                column: "IdConfiguracionTest");

            migrationBuilder.CreateIndex(
                name: "IX_Test_IdEvaluador",
                table: "Test",
                column: "IdEvaluador");

            migrationBuilder.CreateIndex(
                name: "IX_Test_IdPaciente",
                table: "Test",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_TestSecciones_IdConfiguracionSecciones",
                table: "TestSecciones",
                column: "IdConfiguracionSecciones");

            migrationBuilder.CreateIndex(
                name: "IX_TestSecciones_IdTest",
                table: "TestSecciones",
                column: "IdTest");

            migrationBuilder.CreateIndex(
                name: "IX_TestsPreguntas_IdConfiguracionBancoPreguntas",
                table: "TestsPreguntas",
                column: "IdConfiguracionBancoPreguntas");

            migrationBuilder.CreateIndex(
                name: "IX_TestsPreguntas_IdTestSecciones",
                table: "TestsPreguntas",
                column: "IdTestSecciones");

            migrationBuilder.CreateIndex(
                name: "IX_TipoSecciones_IdTipoTest",
                table: "TipoSecciones",
                column: "IdTipoTest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracionesOpciones");

            migrationBuilder.DropTable(
                name: "TestsPreguntas");

            migrationBuilder.DropTable(
                name: "ConfiguracionesPreguntas");

            migrationBuilder.DropTable(
                name: "TestSecciones");

            migrationBuilder.DropTable(
                name: "ConfiguracionesSecciones");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "TipoSecciones");

            migrationBuilder.DropTable(
                name: "ConfiguracionesTest");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Evaluadores");

            migrationBuilder.DropTable(
                name: "TipoTest");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Provincias");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
