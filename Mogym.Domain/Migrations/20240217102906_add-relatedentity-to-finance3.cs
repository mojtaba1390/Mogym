using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addrelatedentitytofinance3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "Finance",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_DiscountId",
                table: "Finance",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PlanId",
                table: "Finance",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Discount_DiscountId",
                table: "Finance",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Plan_PlanId",
                table: "Finance",
                column: "PlanId",
                principalTable: "Plan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Discount_DiscountId",
                table: "Finance");

            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Plan_PlanId",
                table: "Finance");

            migrationBuilder.DropIndex(
                name: "IX_Finance_DiscountId",
                table: "Finance");

            migrationBuilder.DropIndex(
                name: "IX_Finance_PlanId",
                table: "Finance");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "Finance",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
