using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addtargetqtoquestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudingCertificatePic",
                table: "TrainerProfile",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "Question",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Target",
                table: "Question");

            migrationBuilder.AlterColumn<string>(
                name: "StudingCertificatePic",
                table: "TrainerProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);
        }
    }
}
