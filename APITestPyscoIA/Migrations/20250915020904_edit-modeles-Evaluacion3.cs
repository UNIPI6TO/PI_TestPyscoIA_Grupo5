using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class editmodelesEvaluacion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secciones_Evaluaciones_EvaluacionesModelId",
                table: "Secciones");

            migrationBuilder.DropIndex(
                name: "IX_Secciones_EvaluacionesModelId",
                table: "Secciones");

            migrationBuilder.DropColumn(
                name: "EvaluacionesModelId",
                table: "Secciones");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_IdEvaluaciones",
                table: "Secciones",
                column: "IdEvaluaciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Secciones_Evaluaciones_IdEvaluaciones",
                table: "Secciones",
                column: "IdEvaluaciones",
                principalTable: "Evaluaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secciones_Evaluaciones_IdEvaluaciones",
                table: "Secciones");

            migrationBuilder.DropIndex(
                name: "IX_Secciones_IdEvaluaciones",
                table: "Secciones");

            migrationBuilder.AddColumn<int>(
                name: "EvaluacionesModelId",
                table: "Secciones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_EvaluacionesModelId",
                table: "Secciones",
                column: "EvaluacionesModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Secciones_Evaluaciones_EvaluacionesModelId",
                table: "Secciones",
                column: "EvaluacionesModelId",
                principalTable: "Evaluaciones",
                principalColumn: "Id");
        }
    }
}
