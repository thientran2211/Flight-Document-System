using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocSystem.Migrations
{
    public partial class AddRefreshToken2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserID",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GroupID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "Users");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupID",
                table: "Users",
                column: "GroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserID",
                table: "RefreshTokens",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupID",
                table: "Users",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "GroupID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
