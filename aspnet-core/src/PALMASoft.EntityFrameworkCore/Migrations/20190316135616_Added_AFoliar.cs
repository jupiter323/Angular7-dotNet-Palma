using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PALMASoft.Migrations
{
    public partial class Added_AFoliar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AFoliares",
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
                    COD_FOLIAR = table.Column<string>(maxLength: 12, nullable: false),
                    ID_FOLIAR = table.Column<string>(maxLength: 36, nullable: true),
                    MATERIAL_FOLIAR = table.Column<string>(nullable: true),
                    NUM_HOJA_FOLIAR = table.Column<int>(nullable: false),
                    NITROGENO = table.Column<decimal>(nullable: false),
                    FOSFORO = table.Column<decimal>(nullable: false),
                    POTASIO = table.Column<decimal>(nullable: false),
                    CALCIO = table.Column<decimal>(nullable: false),
                    MAGNESIO = table.Column<decimal>(nullable: false),
                    CLORO = table.Column<decimal>(nullable: false),
                    AZUFRE = table.Column<decimal>(nullable: false),
                    BORO = table.Column<decimal>(nullable: false),
                    HIERRO = table.Column<decimal>(nullable: false),
                    COBRE = table.Column<decimal>(nullable: false),
                    MANGANESO = table.Column<decimal>(nullable: false),
                    ZINC = table.Column<decimal>(nullable: false),
                    Ca_Mg_K = table.Column<decimal>(nullable: false),
                    Ca_Mg_div_K = table.Column<decimal>(nullable: false),
                    N_div_K = table.Column<decimal>(nullable: false),
                    N_div_P = table.Column<decimal>(nullable: false),
                    K_div_P = table.Column<decimal>(nullable: false),
                    Ca_div_B = table.Column<decimal>(nullable: false),
                    AnalisisId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AFoliares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AFoliares_Analises_AnalisisId",
                        column: x => x.AnalisisId,
                        principalTable: "Analises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AFoliares_AnalisisId",
                table: "AFoliares",
                column: "AnalisisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AFoliares");
        }
    }
}
