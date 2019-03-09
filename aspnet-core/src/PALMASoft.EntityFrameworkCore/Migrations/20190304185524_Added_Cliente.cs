using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PALMASoft.Migrations
{
    public partial class Added_Cliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
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
                    ID_CLIENTE = table.Column<string>(maxLength: 16, nullable: false),
                    NOMBRE_CLIENTE = table.Column<string>(maxLength: 36, nullable: true),
                    APELLIDO_CLIENTE = table.Column<string>(maxLength: 36, nullable: true),
                    GENERO_CLIENTE = table.Column<string>(nullable: true),
                    FECHA_CLIENTE = table.Column<DateTime>(nullable: true),
                    CELULAR_CLIENTE = table.Column<string>(nullable: true),
                    DIRECCION_CLIENTE = table.Column<string>(nullable: true),
                    DEPARTAMENTO_CLIENTE = table.Column<string>(nullable: true),
                    MUNICIPIO_CLIENTE = table.Column<string>(nullable: true),
                    EMPRESA_CLIENTE = table.Column<string>(nullable: true),
                    PROFESION_CLIENTE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
