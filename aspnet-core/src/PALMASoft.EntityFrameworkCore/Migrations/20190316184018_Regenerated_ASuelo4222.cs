using Microsoft.EntityFrameworkCore.Migrations;

namespace PALMASoft.Migrations
{
    public partial class Regenerated_ASuelo4222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ASuelos_Analises_AnalisisId_SuelId",
                table: "ASuelos");

            migrationBuilder.DropIndex(
                name: "IX_ASuelos_AnalisisId_SuelId",
                table: "ASuelos");

            migrationBuilder.DropColumn(
                name: "AnalisisId_SuelId",
                table: "ASuelos");

            migrationBuilder.DropColumn(
                name: "AnalisisId_Suelos",
                table: "ASuelos");

            migrationBuilder.AddColumn<string>(
                name: "ANALISIS_ID",
                table: "ASuelos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ANALISIS_ID",
                table: "ASuelos");

            migrationBuilder.AddColumn<long>(
                name: "AnalisisId_SuelId",
                table: "ASuelos",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AnalisisId_Suelos",
                table: "ASuelos",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ASuelos_AnalisisId_SuelId",
                table: "ASuelos",
                column: "AnalisisId_SuelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ASuelos_Analises_AnalisisId_SuelId",
                table: "ASuelos",
                column: "AnalisisId_SuelId",
                principalTable: "Analises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
