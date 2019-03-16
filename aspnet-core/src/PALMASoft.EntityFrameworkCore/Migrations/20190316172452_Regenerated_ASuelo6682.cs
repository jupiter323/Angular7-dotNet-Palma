using Microsoft.EntityFrameworkCore.Migrations;

namespace PALMASoft.Migrations
{
    public partial class Regenerated_ASuelo6682 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Analisis_Id",
                table: "ASuelos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ASuelos_Analisis_Id",
                table: "ASuelos",
                column: "Analisis_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ASuelos_Analises_Analisis_Id",
                table: "ASuelos",
                column: "Analisis_Id",
                principalTable: "Analises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ASuelos_Analises_Analisis_Id",
                table: "ASuelos");

            migrationBuilder.DropIndex(
                name: "IX_ASuelos_Analisis_Id",
                table: "ASuelos");

            migrationBuilder.DropColumn(
                name: "Analisis_Id",
                table: "ASuelos");
        }
    }
}
