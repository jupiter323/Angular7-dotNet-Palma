using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PALMASoft.Migrations
{
    public partial class Added_Finca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fincas",
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
                    ID_FINCA = table.Column<string>(maxLength: 12, nullable: false),
                    NOMBRE_FINCA = table.Column<string>(maxLength: 36, nullable: true),
                    DEPARTAMENTO_FINCA = table.Column<string>(nullable: true),
                    MUNICIPIO_FINCA = table.Column<string>(nullable: true),
                    VEREDA_FINCA = table.Column<string>(nullable: true),
                    CORREGIMIENTO_FINCA = table.Column<string>(nullable: true),
                    UBICACION_FINCA = table.Column<string>(nullable: true),
                    LONGITUD_FINCA = table.Column<string>(nullable: true),
                    LATITUD_FINCA = table.Column<string>(nullable: true),
                    CONTACTO_FINCA = table.Column<string>(nullable: true),
                    TELEFONO_FINCA = table.Column<string>(nullable: true),
                    CORREO_FINCA = table.Column<string>(nullable: true),
                    ClienteId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fincas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fincas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fincas_ClienteId",
                table: "Fincas",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fincas");
        }
    }
}
