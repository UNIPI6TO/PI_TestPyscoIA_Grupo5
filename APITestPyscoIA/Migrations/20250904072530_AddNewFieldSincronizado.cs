using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldSincronizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "TipoTest",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "TipoTest",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "TipoSecciones",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "TipoSecciones",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "TestsPreguntas",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "TestsPreguntas",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "TestSecciones",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "TestSecciones",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Test",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "Test",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Provincias",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "Provincias",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Paises",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "Paises",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Pacientes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "Pacientes",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Evaluadores",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "Evaluadores",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "ConfiguracionesTest",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "ConfiguracionesTest",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "ConfiguracionesSecciones",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "ConfiguracionesSecciones",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "ConfiguracionesPreguntas",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "ConfiguracionesPreguntas",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "ConfiguracionesOpciones",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "ConfiguracionesOpciones",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Ciudades",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Sincronizado",
                table: "Ciudades",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "TipoTest");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "TipoSecciones");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "TestsPreguntas");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "TestSecciones");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "Paises");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "Evaluadores");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "ConfiguracionesTest");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "ConfiguracionesSecciones");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "ConfiguracionesPreguntas");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "ConfiguracionesOpciones");

            migrationBuilder.DropColumn(
                name: "Sincronizado",
                table: "Ciudades");

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "TipoTest",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "TipoSecciones",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "TestsPreguntas",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "TestSecciones",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Test",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Provincias",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Paises",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Pacientes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Evaluadores",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "ConfiguracionesTest",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "ConfiguracionesSecciones",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "ConfiguracionesPreguntas",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "ConfiguracionesOpciones",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Ciudades",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
