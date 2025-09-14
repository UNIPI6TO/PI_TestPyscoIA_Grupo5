using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class addIdCiudad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCiudad",
                table: "Evaluadores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluadores_IdCiudad",
                table: "Evaluadores",
                column: "IdCiudad");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluadores_Ciudades_IdCiudad",
                table: "Evaluadores",
                column: "IdCiudad",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluadores_Ciudades_IdCiudad",
                table: "Evaluadores");

            migrationBuilder.DropIndex(
                name: "IX_Evaluadores_IdCiudad",
                table: "Evaluadores");

            migrationBuilder.DropColumn(
                name: "IdCiudad",
                table: "Evaluadores");
        }
    }
}
