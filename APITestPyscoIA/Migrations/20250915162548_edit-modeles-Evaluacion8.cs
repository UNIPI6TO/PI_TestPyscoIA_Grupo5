using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class editmodelesEvaluacion8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaFinTest",
                table: "Secciones");

            migrationBuilder.DropColumn(
                name: "FechaInicioTest",
                table: "Secciones");

            migrationBuilder.DropColumn(
                name: "Resultado",
                table: "Secciones");

            migrationBuilder.AddColumn<string>(
                name: "Seccion",
                table: "Secciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seccion",
                table: "Secciones");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFinTest",
                table: "Secciones",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicioTest",
                table: "Secciones",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resultado",
                table: "Secciones",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
