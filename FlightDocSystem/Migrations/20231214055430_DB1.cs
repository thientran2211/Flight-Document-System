using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocSystem.Migrations
{
    public partial class DB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Flight_FlightID",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Users_UserID",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Permission_PermissionID",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Group_GroupID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                table: "Permission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flight",
                table: "Flight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.RenameTable(
                name: "Permission",
                newName: "Permissions");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Groups");

            migrationBuilder.RenameTable(
                name: "Flight",
                newName: "Flights");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "Documents");

            migrationBuilder.RenameIndex(
                name: "IX_Group_PermissionID",
                table: "Groups",
                newName: "IX_Groups_PermissionID");

            migrationBuilder.RenameIndex(
                name: "IX_Document_UserID",
                table: "Documents",
                newName: "IX_Documents_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Document_FlightID",
                table: "Documents",
                newName: "IX_Documents_FlightID");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Version",
                table: "Documents",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                column: "PermissionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "GroupID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flights",
                table: "Flights",
                column: "FlightID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "DocumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Flights_FlightID",
                table: "Documents",
                column: "FlightID",
                principalTable: "Flights",
                principalColumn: "FlightID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_UserID",
                table: "Documents",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Permissions_PermissionID",
                table: "Groups",
                column: "PermissionID",
                principalTable: "Permissions",
                principalColumn: "PermissionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupID",
                table: "Users",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "GroupID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Flights_FlightID",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_UserID",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Permissions_PermissionID",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flights",
                table: "Flights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "Permission");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "Group");

            migrationBuilder.RenameTable(
                name: "Flights",
                newName: "Flight");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Document");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_PermissionID",
                table: "Group",
                newName: "IX_Group_PermissionID");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_UserID",
                table: "Document",
                newName: "IX_Document_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_FlightID",
                table: "Document",
                newName: "IX_Document_FlightID");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Version",
                table: "Document",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                table: "Permission",
                column: "PermissionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "GroupID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flight",
                table: "Flight",
                column: "FlightID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "DocumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Flight_FlightID",
                table: "Document",
                column: "FlightID",
                principalTable: "Flight",
                principalColumn: "FlightID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Users_UserID",
                table: "Document",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Permission_PermissionID",
                table: "Group",
                column: "PermissionID",
                principalTable: "Permission",
                principalColumn: "PermissionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Group_GroupID",
                table: "Users",
                column: "GroupID",
                principalTable: "Group",
                principalColumn: "GroupID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
