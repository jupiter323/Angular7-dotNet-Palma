using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PALMASoft.Migrations
{
    public partial class Added_ASuelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ASuelos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    COD_SUELOS = table.Column<string>(nullable: true),
                    ID_SUELOS = table.Column<string>(nullable: true),
                    MATERIAL_SUELOS = table.Column<string>(nullable: true),
                    PROFUNDIDAD_MUESTRA = table.Column<int>(nullable: false),
                    TEXTURA_SUELOS = table.Column<string>(nullable: true),
                    ARENA = table.Column<decimal>(nullable: false),
                    LIMO = table.Column<decimal>(nullable: false),
                    ARCILLA = table.Column<decimal>(nullable: false),
                    PH = table.Column<decimal>(nullable: false),
                    CARBONO_ORGANICO = table.Column<decimal>(nullable: false),
                    MATERIA_ORGANICA = table.Column<decimal>(nullable: false),
                    FOSFORO = table.Column<decimal>(nullable: false),
                    AZUFRE = table.Column<decimal>(nullable: false),
                    ACIDEZ = table.Column<decimal>(nullable: true),
                    ALUMINIO = table.Column<decimal>(nullable: true),
                    CALCIO = table.Column<decimal>(nullable: false),
                    MAGNESIO = table.Column<decimal>(nullable: false),
                    POTASIO = table.Column<decimal>(nullable: false),
                    SODIO = table.Column<decimal>(nullable: false),
                    CATIONICO = table.Column<decimal>(nullable: false),
                    ELECTRICA = table.Column<decimal>(nullable: false),
                    BORO = table.Column<decimal>(nullable: false),
                    HIERRO = table.Column<decimal>(nullable: false),
                    COBRE = table.Column<decimal>(nullable: false),
                    MANGANESO = table.Column<decimal>(nullable: false),
                    ZINC = table.Column<decimal>(nullable: false),
                    CICE = table.Column<decimal>(nullable: false),
                    SUMA_BASES = table.Column<decimal>(nullable: false),
                    SAT_BASES = table.Column<decimal>(nullable: false),
                    SAT_K = table.Column<decimal>(nullable: false),
                    SAT_CA = table.Column<decimal>(nullable: false),
                    SAT_MG = table.Column<decimal>(nullable: false),
                    SAT_NA = table.Column<decimal>(nullable: false),
                    SAT_AL = table.Column<decimal>(nullable: false),
                    CA_MG = table.Column<decimal>(nullable: false),
                    K_MG = table.Column<decimal>(nullable: false),
                    CA_MG_DIV_K = table.Column<decimal>(nullable: false),
                    AnalisisId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASuelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ASuelos_Analises_AnalisisId",
                        column: x => x.AnalisisId,
                        principalTable: "Analises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASuelos_AnalisisId",
                table: "ASuelos",
                column: "AnalisisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASuelos");
        }
    }
}
