using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class editmodelesEvaluacion5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preguntas_ConfiguracionesPreguntas_IdConfiguracionBancoPreguntas",
                table: "Preguntas");

            migrationBuilder.RenameColumn(
                name: "IdConfiguracionBancoPreguntas",
                table: "Preguntas",
                newName: "IdConfiguracionPreguntas");

            migrationBuilder.RenameIndex(
                name: "IX_Preguntas_IdConfiguracionBancoPreguntas",
                table: "Preguntas",
                newName: "IX_Preguntas_IdConfiguracionPreguntas");

            migrationBuilder.AddForeignKey(
                name: "FK_Preguntas_ConfiguracionesPreguntas_IdConfiguracionPreguntas",
                table: "Preguntas",
                column: "IdConfiguracionPreguntas",
                principalTable: "ConfiguracionesPreguntas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preguntas_ConfiguracionesPreguntas_IdConfiguracionPreguntas",
                table: "Preguntas");

            migrationBuilder.RenameColumn(
                name: "IdConfiguracionPreguntas",
                table: "Preguntas",
                newName: "IdConfiguracionBancoPreguntas");

            migrationBuilder.RenameIndex(
                name: "IX_Preguntas_IdConfiguracionPreguntas",
                table: "Preguntas",
                newName: "IX_Preguntas_IdConfiguracionBancoPreguntas");

            migrationBuilder.AddForeignKey(
                name: "FK_Preguntas_ConfiguracionesPreguntas_IdConfiguracionBancoPreguntas",
                table: "Preguntas",
                column: "IdConfiguracionBancoPreguntas",
                principalTable: "ConfiguracionesPreguntas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
