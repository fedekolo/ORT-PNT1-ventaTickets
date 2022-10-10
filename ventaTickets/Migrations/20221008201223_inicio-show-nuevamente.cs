using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ventaTickets.Migrations
{
    public partial class inicioshownuevamente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Show",
                newName: "showId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "showId",
                table: "Show",
                newName: "Id");
        }
    }
}
