using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class editmodelesEvaluacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestsPreguntas");

            migrationBuilder.DropTable(
                name: "TestSecciones");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.CreateTable(
                name: "Evaluaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantidadPreguntas = table.Column<int>(type: "int", nullable: false),
                    IdConfiguracionTest = table.Column<int>(type: "int", nullable: false),
                    IdEvaluador = table.Column<int>(type: "int", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: true),
                    Contestadas = table.Column<int>(type: "int", nullable: true),
                    NoContestadas = table.Column<int>(type: "int", nullable: true),
                    Completado = table.Column<bool>(type: "bit", nullable: true),
                    Iniciado = table.Column<bool>(type: "bit", nullable: true),
                    TiempoTranscurrido = table.Column<int>(type: "int", nullable: true),
                    FechaInicioTest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinTest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: true),
                    Sincronizado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluaciones_ConfiguracionesTest_IdConfiguracionTest",
                        column: x => x.IdConfiguracionTest,
                        principalTable: "ConfiguracionesTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluaciones_Evaluadores_IdEvaluador",
                        column: x => x.IdEvaluador,
                        principalTable: "Evaluadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluaciones_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Secciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<float>(type: "real", nullable: true),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicioTest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinTest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdEvaluaciones = table.Column<int>(type: "int", nullable: false),
                    IdConfiguracionSecciones = table.Column<int>(type: "int", nullable: false),
                    EvaluacionId = table.Column<int>(type: "int", nullable: true),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: true),
                    Sincronizado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Secciones_ConfiguracionesSecciones_IdConfiguracionSecciones",
                        column: x => x.IdConfiguracionSecciones,
                        principalTable: "ConfiguracionesSecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Secciones_Evaluaciones_EvaluacionId",
                        column: x => x.EvaluacionId,
                        principalTable: "Evaluaciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Preguntas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(8,4)", precision: 8, scale: 4, nullable: false),
                    IdConfiguracionBancoPreguntas = table.Column<int>(type: "int", nullable: false),
                    IdSecciones = table.Column<int>(type: "int", nullable: false),
                    TestSeccionesId = table.Column<int>(type: "int", nullable: true),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: true),
                    Sincronizado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preguntas_ConfiguracionesPreguntas_IdConfiguracionBancoPreguntas",
                        column: x => x.IdConfiguracionBancoPreguntas,
                        principalTable: "ConfiguracionesPreguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Preguntas_Secciones_TestSeccionesId",
                        column: x => x.TestSeccionesId,
                        principalTable: "Secciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluaciones_IdConfiguracionTest",
                table: "Evaluaciones",
                column: "IdConfiguracionTest");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluaciones_IdEvaluador",
                table: "Evaluaciones",
                column: "IdEvaluador");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluaciones_IdPaciente",
                table: "Evaluaciones",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_IdConfiguracionBancoPreguntas",
                table: "Preguntas",
                column: "IdConfiguracionBancoPreguntas");

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_TestSeccionesId",
                table: "Preguntas",
                column: "TestSeccionesId");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_EvaluacionId",
                table: "Secciones",
                column: "EvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_IdConfiguracionSecciones",
                table: "Secciones",
                column: "IdConfiguracionSecciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Preguntas");

            migrationBuilder.DropTable(
                name: "Secciones");

            migrationBuilder.DropTable(
                name: "Evaluaciones");

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdConfiguracionTest = table.Column<int>(type: "int", nullable: false),
                    IdEvaluador = table.Column<int>(type: "int", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CantidadPreguntas = table.Column<int>(type: "int", nullable: false),
                    Completado = table.Column<bool>(type: "bit", nullable: true),
                    Contestadas = table.Column<int>(type: "int", nullable: true),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: true),
                    Iniciado = table.Column<bool>(type: "bit", nullable: true),
                    NoContestadas = table.Column<int>(type: "int", nullable: true),
                    Sincronizado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_ConfiguracionesTest_IdConfiguracionTest",
                        column: x => x.IdConfiguracionTest,
                        principalTable: "ConfiguracionesTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Test_Evaluadores_IdEvaluador",
                        column: x => x.IdEvaluador,
                        principalTable: "Evaluadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Test_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestSecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdConfiguracionSecciones = table.Column<int>(type: "int", nullable: false),
                    IdTest = table.Column<int>(type: "int", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: true),
                    FechaFinTest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaInicioTest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: true),
                    Sincronizado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSecciones_ConfiguracionesSecciones_IdConfiguracionSecciones",
                        column: x => x.IdConfiguracionSecciones,
                        principalTable: "ConfiguracionesSecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestSecciones_Test_IdTest",
                        column: x => x.IdTest,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestsPreguntas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdConfiguracionBancoPreguntas = table.Column<int>(type: "int", nullable: false),
                    IdTestSecciones = table.Column<int>(type: "int", nullable: false),
                    Actualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: true),
                    Peso = table.Column<decimal>(type: "decimal(8,4)", precision: 8, scale: 4, nullable: false),
                    Pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sincronizado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestsPreguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestsPreguntas_ConfiguracionesPreguntas_IdConfiguracionBancoPreguntas",
                        column: x => x.IdConfiguracionBancoPreguntas,
                        principalTable: "ConfiguracionesPreguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestsPreguntas_TestSecciones_IdTestSecciones",
                        column: x => x.IdTestSecciones,
                        principalTable: "TestSecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }
    }
}
