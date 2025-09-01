using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class ModiciacionConfigTestModel102 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orden",
                table: "TipoSecciones");

            migrationBuilder.DropColumn(
                name: "CantidadPreguntas",
                table: "TestSecciones");

            migrationBuilder.DropColumn(
                name: "Completado",
                table: "TestSecciones");

            migrationBuilder.DropColumn(
                name: "Contestadas",
                table: "TestSecciones");

            migrationBuilder.DropColumn(
                name: "Duracion",
                table: "TestSecciones");

            migrationBuilder.DropColumn(
                name: "Iniciado",
                table: "TestSecciones");

            migrationBuilder.DropColumn(
                name: "NoContestadas",
                table: "TestSecciones");

            migrationBuilder.AddColumn<bool>(
                name: "Completado",
                table: "Test",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Contestadas",
                table: "Test",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duracion",
                table: "Test",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Iniciado",
                table: "Test",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoContestadas",
                table: "Test",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duracion",
                table: "ConfiguracionesTest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completado",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Contestadas",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Duracion",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Iniciado",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "NoContestadas",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Duracion",
                table: "ConfiguracionesTest");

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "TipoSecciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CantidadPreguntas",
                table: "TestSecciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Completado",
                table: "TestSecciones",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Contestadas",
                table: "TestSecciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duracion",
                table: "TestSecciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Iniciado",
                table: "TestSecciones",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoContestadas",
                table: "TestSecciones",
                type: "int",
                nullable: true);
        }
    }
}
