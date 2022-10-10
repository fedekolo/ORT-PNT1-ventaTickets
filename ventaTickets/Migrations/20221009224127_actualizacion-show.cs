using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ventaTickets.Migrations
{
    public partial class actualizacionshow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cantPublico",
                table: "Show",
                newName: "precioPlateaAlta");

            migrationBuilder.AddColumn<int>(
                name: "cantCampo",
                table: "Show",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cantPlateaAlta",
                table: "Show",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cantPlateaBaja",
                table: "Show",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "precioCampo",
                table: "Show",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "precioPlateaBaja",
                table: "Show",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cantCampo",
                table: "Show");

            migrationBuilder.DropColumn(
                name: "cantPlateaAlta",
                table: "Show");

            migrationBuilder.DropColumn(
                name: "cantPlateaBaja",
                table: "Show");

            migrationBuilder.DropColumn(
                name: "precioCampo",
                table: "Show");

            migrationBuilder.DropColumn(
                name: "precioPlateaBaja",
                table: "Show");

            migrationBuilder.RenameColumn(
                name: "precioPlateaAlta",
                table: "Show",
                newName: "cantPublico");
        }
    }
}
