using Microsoft.EntityFrameworkCore.Migrations;

namespace PALMASoft.Migrations
{
    public partial class Regenerated_ASuelo3169 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ASuelos_Analises_Analisis_Id",
                table: "ASuelos");

            migrationBuilder.RenameColumn(
                name: "Analisis_Id",
                table: "ASuelos",
                newName: "AnalisisId_SuelId");

            migrationBuilder.RenameIndex(
                name: "IX_ASuelos_Analisis_Id",
                table: "ASuelos",
                newName: "IX_ASuelos_AnalisisId_SuelId");

            migrationBuilder.AddColumn<long>(
                name: "AnalisisId_Suelos",
                table: "ASuelos",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_ASuelos_Analises_AnalisisId_SuelId",
                table: "ASuelos",
                column: "AnalisisId_SuelId",
                principalTable: "Analises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ASuelos_Analises_AnalisisId_SuelId",
                table: "ASuelos");

            migrationBuilder.DropColumn(
                name: "AnalisisId_Suelos",
                table: "ASuelos");

            migrationBuilder.RenameColumn(
                name: "AnalisisId_SuelId",
                table: "ASuelos",
                newName: "Analisis_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ASuelos_AnalisisId_SuelId",
                table: "ASuelos",
                newName: "IX_ASuelos_Analisis_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ASuelos_Analises_Analisis_Id",
                table: "ASuelos",
                column: "Analisis_Id",
                principalTable: "Analises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
