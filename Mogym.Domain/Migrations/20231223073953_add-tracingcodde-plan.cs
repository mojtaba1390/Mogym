using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addtracingcoddeplan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "TrackingCode",
                table: "Plan",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackingCode",
                table: "Plan");

            migrationBuilder.CreateTable(
                name: "SmsLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    cost = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<long>(type: "BIGINT", nullable: true),
                    message = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    messageid = table.Column<long>(type: "BIGINT", nullable: true),
                    receptor = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    sender = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    statustext = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsLog", x => x.Id);
                });
        }
    }
}
