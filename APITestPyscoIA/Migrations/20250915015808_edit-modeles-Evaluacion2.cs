using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class editmodelesEvaluacion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secciones_Evaluaciones_EvaluacionId",
                table: "Secciones");

            migrationBuilder.RenameColumn(
                name: "EvaluacionId",
                table: "Secciones",
                newName: "EvaluacionesModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Secciones_EvaluacionId",
                table: "Secciones",
                newName: "IX_Secciones_EvaluacionesModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Secciones_Evaluaciones_EvaluacionesModelId",
                table: "Secciones",
                column: "EvaluacionesModelId",
                principalTable: "Evaluaciones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secciones_Evaluaciones_EvaluacionesModelId",
                table: "Secciones");

            migrationBuilder.RenameColumn(
                name: "EvaluacionesModelId",
                table: "Secciones",
                newName: "EvaluacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Secciones_EvaluacionesModelId",
                table: "Secciones",
                newName: "IX_Secciones_EvaluacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Secciones_Evaluaciones_EvaluacionId",
                table: "Secciones",
                column: "EvaluacionId",
                principalTable: "Evaluaciones",
                principalColumn: "Id");
        }
    }
}
