using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class adduseridexerciseVideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ExerciseVideo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseVideo_UserId",
                table: "ExerciseVideo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseVideo_User_UserId",
                table: "ExerciseVideo",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseVideo_User_UserId",
                table: "ExerciseVideo");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseVideo_UserId",
                table: "ExerciseVideo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExerciseVideo");
        }
    }
}
