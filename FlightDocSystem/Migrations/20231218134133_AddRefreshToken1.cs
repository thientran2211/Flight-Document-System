using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocSystem.Migrations
{
    public partial class AddRefreshToken1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RefreshTokens",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserID",
                table: "RefreshTokens",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserID",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "RefreshTokens",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserID",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
