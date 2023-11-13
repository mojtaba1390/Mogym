using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class changprofilepictouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "TrainerProfile");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "User",
                type: "nvarchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "TrainerProfile",
                type: "nvarchar(100)",
                nullable: true);
        }
    }
}
