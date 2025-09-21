using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class AddAI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resultado",
                table: "Secciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResultadosAI",
                table: "Evaluaciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "Evaluaciones",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resultado",
                table: "Secciones");

            migrationBuilder.DropColumn(
                name: "ResultadosAI",
                table: "Evaluaciones");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Evaluaciones");
        }
    }
}
