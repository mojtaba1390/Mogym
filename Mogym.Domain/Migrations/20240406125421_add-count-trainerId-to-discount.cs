using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addcounttrainerIdtodiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Discount",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UseCount",
                table: "Discount",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discount_TrainerId",
                table: "Discount",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_TrainerProfile_TrainerId",
                table: "Discount",
                column: "TrainerId",
                principalTable: "TrainerProfile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_TrainerProfile_TrainerId",
                table: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_Discount_TrainerId",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "UseCount",
                table: "Discount");
        }
    }
}
