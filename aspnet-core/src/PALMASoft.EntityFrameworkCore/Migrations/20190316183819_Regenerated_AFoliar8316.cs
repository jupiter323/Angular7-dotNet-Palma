using Microsoft.EntityFrameworkCore.Migrations;

namespace PALMASoft.Migrations
{
    public partial class Regenerated_AFoliar8316 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AFoliares_Analises_AnalisisId_FoliId",
                table: "AFoliares");

            migrationBuilder.DropIndex(
                name: "IX_AFoliares_AnalisisId_FoliId",
                table: "AFoliares");

            migrationBuilder.DropColumn(
                name: "AnalisisId_FoliId",
                table: "AFoliares");

            migrationBuilder.DropColumn(
                name: "AnalisisId_Foliar",
                table: "AFoliares");

            migrationBuilder.AddColumn<string>(
                name: "ANALISIS_ID",
                table: "AFoliares",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ANALISIS_ID",
                table: "AFoliares");

            migrationBuilder.AddColumn<long>(
                name: "AnalisisId_FoliId",
                table: "AFoliares",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AnalisisId_Foliar",
                table: "AFoliares",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AFoliares_AnalisisId_FoliId",
                table: "AFoliares",
                column: "AnalisisId_FoliId");

            migrationBuilder.AddForeignKey(
                name: "FK_AFoliares_Analises_AnalisisId_FoliId",
                table: "AFoliares",
                column: "AnalisisId_FoliId",
                principalTable: "Analises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
