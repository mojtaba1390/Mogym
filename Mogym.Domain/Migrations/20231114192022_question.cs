using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class question : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Waist = table.Column<double>(type: "float", nullable: true),
                    Biceps = table.Column<double>(type: "float", nullable: true),
                    Chest = table.Column<double>(type: "float", nullable: true),
                    Thigh = table.Column<double>(type: "float", nullable: true),
                    Fist = table.Column<double>(type: "float", nullable: true),
                    DailyAvtivity = table.Column<int>(type: "int", nullable: true),
                    NightWork = table.Column<int>(type: "int", nullable: true),
                    OutOfGymActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disease = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Medicine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Treated = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Injury = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HeartDisease = table.Column<int>(type: "int", nullable: true),
                    DiabetesAsthmaHypoglycemia = table.Column<int>(type: "int", nullable: true),
                    Smoke = table.Column<int>(type: "int", nullable: true),
                    SessionsInWeek = table.Column<int>(type: "int", nullable: true),
                    Expection = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
