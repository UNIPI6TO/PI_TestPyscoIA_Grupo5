using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITestPyscoIA.Migrations
{
    /// <inheritdoc />
    public partial class ModiciacionConfigTestModel101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfiguracionesTest_Evaluadores_IdEvaluador",
                table: "ConfiguracionesTest");

            migrationBuilder.DropIndex(
                name: "IX_ConfiguracionesTest_IdEvaluador",
                table: "ConfiguracionesTest");

            migrationBuilder.DropColumn(
                name: "IdEvaluador",
                table: "ConfiguracionesTest");

            migrationBuilder.AddColumn<int>(
                name: "EvaluadorId",
                table: "ConfiguracionesTest",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionesTest_EvaluadorId",
                table: "ConfiguracionesTest",
                column: "EvaluadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfiguracionesTest_Evaluadores_EvaluadorId",
                table: "ConfiguracionesTest",
                column: "EvaluadorId",
                principalTable: "Evaluadores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfiguracionesTest_Evaluadores_EvaluadorId",
                table: "ConfiguracionesTest");

            migrationBuilder.DropIndex(
                name: "IX_ConfiguracionesTest_EvaluadorId",
                table: "ConfiguracionesTest");

            migrationBuilder.DropColumn(
                name: "EvaluadorId",
                table: "ConfiguracionesTest");

            migrationBuilder.AddColumn<int>(
                name: "IdEvaluador",
                table: "ConfiguracionesTest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionesTest_IdEvaluador",
                table: "ConfiguracionesTest",
                column: "IdEvaluador");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfiguracionesTest_Evaluadores_IdEvaluador",
                table: "ConfiguracionesTest",
                column: "IdEvaluador",
                principalTable: "Evaluadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
