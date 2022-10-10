using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ventaTickets.Migrations
{
    public partial class inicioshows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShowId",
                table: "Entrada",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Show",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    direccion = table.Column<string>(type: "TEXT", nullable: false),
                    imagen = table.Column<string>(type: "TEXT", nullable: false),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Show", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entrada_ShowId",
                table: "Entrada",
                column: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrada_Show_ShowId",
                table: "Entrada",
                column: "ShowId",
                principalTable: "Show",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrada_Show_ShowId",
                table: "Entrada");

            migrationBuilder.DropTable(
                name: "Show");

            migrationBuilder.DropIndex(
                name: "IX_Entrada_ShowId",
                table: "Entrada");

            migrationBuilder.DropColumn(
                name: "ShowId",
                table: "Entrada");
        }
    }
}
