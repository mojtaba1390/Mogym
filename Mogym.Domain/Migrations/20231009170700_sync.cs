using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class sync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeriLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogEvent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NationalCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Mobile = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UniqeUserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    BirthDay = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    SmsConfirmCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accessibility",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessibility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accessibility_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accessibility_RoleId",
                table: "Accessibility",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessibility");

            migrationBuilder.DropTable(
                name: "SeriLog");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
