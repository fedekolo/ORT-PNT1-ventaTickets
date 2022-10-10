using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ventaTickets.Migrations
{
    public partial class agregoshowaentrada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idShow",
                table: "Entrada");

            migrationBuilder.AddColumn<int>(
                name: "showId",
                table: "Entrada",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entrada_showId",
                table: "Entrada",
                column: "showId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrada_Show_showId",
                table: "Entrada",
                column: "showId",
                principalTable: "Show",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrada_Show_showId",
                table: "Entrada");

            migrationBuilder.DropIndex(
                name: "IX_Entrada_showId",
                table: "Entrada");

            migrationBuilder.DropColumn(
                name: "showId",
                table: "Entrada");

            migrationBuilder.AddColumn<int>(
                name: "idShow",
                table: "Entrada",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
