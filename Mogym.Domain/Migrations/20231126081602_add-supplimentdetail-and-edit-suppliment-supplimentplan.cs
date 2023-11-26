using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addsupplimentdetailandeditsupplimentsupplimentplan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplimentPlan_Suppliment_SupplimentId",
                table: "SupplimentPlan");

            migrationBuilder.DropIndex(
                name: "IX_SupplimentPlan_SupplimentId",
                table: "SupplimentPlan");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SupplimentPlan");

            migrationBuilder.DropColumn(
                name: "Scale",
                table: "SupplimentPlan");

            migrationBuilder.DropColumn(
                name: "SupplimentId",
                table: "SupplimentPlan");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SupplimentPlan",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SupplimentPlanDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplimentPlanId = table.Column<int>(type: "int", nullable: false),
                    SupplimentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Scale = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplimentPlanDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplimentPlanDetail_SupplimentPlan_SupplimentPlanId",
                        column: x => x.SupplimentPlanId,
                        principalTable: "SupplimentPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplimentPlanDetail_Suppliment_SupplimentId",
                        column: x => x.SupplimentId,
                        principalTable: "Suppliment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplimentPlanDetail_SupplimentId",
                table: "SupplimentPlanDetail",
                column: "SupplimentId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplimentPlanDetail_SupplimentPlanId",
                table: "SupplimentPlanDetail",
                column: "SupplimentPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplimentPlanDetail");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SupplimentPlan");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "SupplimentPlan",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Scale",
                table: "SupplimentPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplimentId",
                table: "SupplimentPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SupplimentPlan_SupplimentId",
                table: "SupplimentPlan",
                column: "SupplimentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplimentPlan_Suppliment_SupplimentId",
                table: "SupplimentPlan",
                column: "SupplimentId",
                principalTable: "Suppliment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
