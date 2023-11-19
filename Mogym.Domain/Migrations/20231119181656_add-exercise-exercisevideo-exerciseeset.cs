using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addexerciseexercisevideoexerciseeset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciseVideo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseVideo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuperSetId = table.Column<int>(type: "int", nullable: true),
                    ExerciseVideoId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_ExerciseVideo_ExerciseVideoId",
                        column: x => x.ExerciseVideoId,
                        principalTable: "ExerciseVideo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Exercise_Exercise_SuperSetId",
                        column: x => x.SuperSetId,
                        principalTable: "Exercise",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: true),
                    Minute = table.Column<int>(type: "int", nullable: true),
                    Second = table.Column<int>(type: "int", nullable: true),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSet_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_ExerciseVideoId",
                table: "Exercise",
                column: "ExerciseVideoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_SuperSetId",
                table: "Exercise",
                column: "SuperSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSet_ExerciseId",
                table: "ExerciseSet",
                column: "ExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseSet");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "ExerciseVideo");
        }
    }
}
