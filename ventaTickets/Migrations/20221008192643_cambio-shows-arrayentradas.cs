using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ventaTickets.Migrations
{
    public partial class cambioshowsarrayentradas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrada_Show_ShowId",
                table: "Entrada");

            migrationBuilder.DropIndex(
                name: "IX_Entrada_ShowId",
                table: "Entrada");

            migrationBuilder.DropColumn(
                name: "ShowId",
                table: "Entrada");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShowId",
                table: "Entrada",
                type: "INTEGER",
                nullable: true);

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
    }
}
