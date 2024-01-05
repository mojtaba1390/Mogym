using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addcodequestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Question",
                type: "char(5)",
                fixedLength: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Question");

            migrationBuilder.CreateIndex(
                name: "IX_Question_TrainerPlan",
                table: "Question",
                column: "TrainerPlan");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_TrainerPlanCost_TrainerPlan",
                table: "Question",
                column: "TrainerPlan",
                principalTable: "TrainerPlanCost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
