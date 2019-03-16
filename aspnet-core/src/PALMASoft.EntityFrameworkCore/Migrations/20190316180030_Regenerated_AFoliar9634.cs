using Microsoft.EntityFrameworkCore.Migrations;

namespace PALMASoft.Migrations
{
    public partial class Regenerated_AFoliar9634 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AFoliares_Analises_AnalisisId",
                table: "AFoliares");

            migrationBuilder.DropIndex(
                name: "IX_AFoliares_AnalisisId",
                table: "AFoliares");

            migrationBuilder.RenameColumn(
                name: "AnalisisId",
                table: "AFoliares",
                newName: "AnalisisId_Foliar");

            migrationBuilder.AddColumn<long>(
                name: "AnalisisId_FoliId",
                table: "AFoliares",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "AnalisisId_Foliar",
                table: "AFoliares",
                newName: "AnalisisId");

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
