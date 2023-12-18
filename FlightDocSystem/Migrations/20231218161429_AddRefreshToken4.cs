using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocSystem.Migrations
{
    public partial class AddRefreshToken4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                 name: "RoleId",
                 table: "Users");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Documents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserID",
                table: "Documents",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_UserID",
                table: "Documents",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
