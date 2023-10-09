using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addusertoroleandaccebility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Accessibility",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Role_UserId",
                table: "Role",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Accessibility_UserId",
                table: "Accessibility",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessibility_User_UserId",
                table: "Accessibility",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetDefault);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_User_UserId",
                table: "Role",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetDefault);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessibility_User_UserId",
                table: "Accessibility");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_User_UserId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_UserId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Accessibility_UserId",
                table: "Accessibility");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Accessibility");
        }
    }
}
