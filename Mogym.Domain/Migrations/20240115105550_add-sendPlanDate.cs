using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addsendPlanDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SendPlanDate",
                table: "Plan",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendPlanDate",
                table: "Plan");
        }
    }
}
