using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class chnageexerciseexercisevideorelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_ExerciseVideo_ExerciseVideoId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_ExerciseVideoId",
                table: "Exercise");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_ExerciseVideoId",
                table: "Exercise",
                column: "ExerciseVideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_ExerciseVideo_ExerciseVideoId",
                table: "Exercise",
                column: "ExerciseVideoId",
                principalTable: "ExerciseVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_ExerciseVideo_ExerciseVideoId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_ExerciseVideoId",
                table: "Exercise");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_ExerciseVideoId",
                table: "Exercise",
                column: "ExerciseVideoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_ExerciseVideo_ExerciseVideoId",
                table: "Exercise",
                column: "ExerciseVideoId",
                principalTable: "ExerciseVideo",
                principalColumn: "Id");
        }
    }
}
