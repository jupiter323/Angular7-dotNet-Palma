using Microsoft.EntityFrameworkCore.Migrations;

namespace PALMASoft.Migrations
{
    public partial class Regenerated_ASuelo6379 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ASuelos_Analises_AnalisisId",
                table: "ASuelos");

            migrationBuilder.DropIndex(
                name: "IX_ASuelos_AnalisisId",
                table: "ASuelos");

            migrationBuilder.DropColumn(
                name: "AnalisisId",
                table: "ASuelos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AnalisisId",
                table: "ASuelos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ASuelos_AnalisisId",
                table: "ASuelos",
                column: "AnalisisId");

            migrationBuilder.AddForeignKey(
                name: "FK_ASuelos_Analises_AnalisisId",
                table: "ASuelos",
                column: "AnalisisId",
                principalTable: "Analises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
