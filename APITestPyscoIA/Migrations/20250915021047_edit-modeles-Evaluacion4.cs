using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class editmodelesEvaluacion4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preguntas_Secciones_TestSeccionesId",
                table: "Preguntas");

            migrationBuilder.DropIndex(
                name: "IX_Preguntas_TestSeccionesId",
                table: "Preguntas");

            migrationBuilder.DropColumn(
                name: "TestSeccionesId",
                table: "Preguntas");

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_IdSecciones",
                table: "Preguntas",
                column: "IdSecciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Preguntas_Secciones_IdSecciones",
                table: "Preguntas",
                column: "IdSecciones",
                principalTable: "Secciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preguntas_Secciones_IdSecciones",
                table: "Preguntas");

            migrationBuilder.DropIndex(
                name: "IX_Preguntas_IdSecciones",
                table: "Preguntas");

            migrationBuilder.AddColumn<int>(
                name: "TestSeccionesId",
                table: "Preguntas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_TestSeccionesId",
                table: "Preguntas",
                column: "TestSeccionesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Preguntas_Secciones_TestSeccionesId",
                table: "Preguntas",
                column: "TestSeccionesId",
                principalTable: "Secciones",
                principalColumn: "Id");
        }
    }
}
