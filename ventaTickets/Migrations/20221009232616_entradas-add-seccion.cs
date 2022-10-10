using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ventaTickets.Migrations
{
    public partial class entradasaddseccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sector",
                table: "Entrada",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sector",
                table: "Entrada");
        }
    }
}
