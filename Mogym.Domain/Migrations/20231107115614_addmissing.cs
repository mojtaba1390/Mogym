﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addmissing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "TrainerProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerProfile_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });



            migrationBuilder.CreateTable(
                name: "TrainerAchievement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Date = table.Column<int>(type: "int", nullable: true),
                    TrainerProfileId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerAchievement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerAchievement_TrainerProfile_TrainerProfileId",
                        column: x => x.TrainerProfileId,
                        principalTable: "TrainerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerPlanCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerPlan = table.Column<int>(type: "int", nullable: false),
                    OriginalCost = table.Column<double>(type: "float", nullable: true),
                    SaleCost = table.Column<double>(type: "float", nullable: true),
                    TrainerProfileId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerPlanCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerPlanCost_TrainerProfile_TrainerProfileId",
                        column: x => x.TrainerProfileId,
                        principalTable: "TrainerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });







        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {



            migrationBuilder.DropTable(
                name: "TrainerAchievement");

            migrationBuilder.DropTable(
                name: "TrainerPlanCost");




            migrationBuilder.DropTable(
                name: "TrainerProfile");
        }
    }
}