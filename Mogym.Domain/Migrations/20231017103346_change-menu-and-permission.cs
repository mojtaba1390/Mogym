using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class changemenuandpermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCreateMenu",
                table: "Permission",
                newName: "ParentId");

            migrationBuilder.AddColumn<int>(
                name: "HasParentInPermission",
                table: "Menu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ParentId",
                table: "Permission",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_Permission_ParentId",
                table: "Permission",
                column: "ParentId",
                principalTable: "Permission",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permission_Permission_ParentId",
                table: "Permission");

            migrationBuilder.DropIndex(
                name: "IX_Permission_ParentId",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "HasParentInPermission",
                table: "Menu");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Permission",
                newName: "IsCreateMenu");
        }
    }
}
