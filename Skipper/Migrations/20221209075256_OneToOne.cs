using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skipper.Migrations
{
    /// <inheritdoc />
    public partial class OneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserSettings",
                table: "UsersSettings");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSettings_Users_UserId",
                table: "UsersSettings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersSettings_Users_UserId",
                table: "UsersSettings");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserSettings",
                table: "UsersSettings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
