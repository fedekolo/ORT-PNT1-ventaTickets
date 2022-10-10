using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ventaTickets.Migrations
{
    public partial class entradasconidshow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrada_Show_showId",
                table: "Entrada");

            migrationBuilder.AlterColumn<int>(
                name: "showId",
                table: "Entrada",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entrada_Show_showId",
                table: "Entrada",
                column: "showId",
                principalTable: "Show",
                principalColumn: "showId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrada_Show_showId",
                table: "Entrada");

            migrationBuilder.AlterColumn<int>(
                name: "showId",
                table: "Entrada",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrada_Show_showId",
                table: "Entrada",
                column: "showId",
                principalTable: "Show",
                principalColumn: "showId");
        }
    }
}
