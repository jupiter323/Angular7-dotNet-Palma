using Microsoft.EntityFrameworkCore.Migrations;

namespace PALMASoft.Migrations
{
    public partial class Regenerated_AFoliar2569 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AFoliares_Analises_AnalisisId",
                table: "AFoliares");

            migrationBuilder.DropIndex(
                name: "IX_AFoliares_AnalisisId",
                table: "AFoliares");

            migrationBuilder.DropColumn(
                name: "AnalisisId",
                table: "AFoliares");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AnalisisId",
                table: "AFoliares",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AFoliares_AnalisisId",
                table: "AFoliares",
                column: "AnalisisId");

            migrationBuilder.AddForeignKey(
                name: "FK_AFoliares_Analises_AnalisisId",
                table: "AFoliares",
                column: "AnalisisId",
                principalTable: "Analises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
