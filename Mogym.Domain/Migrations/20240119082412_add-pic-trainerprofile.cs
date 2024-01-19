using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addpictrainerprofile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChampCertificatePic1",
                table: "TrainerProfile",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChampCertificatePic2",
                table: "TrainerProfile",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChampCertificatePic3",
                table: "TrainerProfile",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GraduationCertificatePic",
                table: "TrainerProfile",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalCartPic",
                table: "TrainerProfile",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudingCertificatePic",
                table: "TrainerProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingCertificatePic",
                table: "TrainerProfile",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingCertificatePic",
                table: "TrainerProfile",
                type: "nvarchar(200)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChampCertificatePic1",
                table: "TrainerProfile");

            migrationBuilder.DropColumn(
                name: "ChampCertificatePic2",
                table: "TrainerProfile");

            migrationBuilder.DropColumn(
                name: "ChampCertificatePic3",
                table: "TrainerProfile");

            migrationBuilder.DropColumn(
                name: "GraduationCertificatePic",
                table: "TrainerProfile");

            migrationBuilder.DropColumn(
                name: "NationalCartPic",
                table: "TrainerProfile");

            migrationBuilder.DropColumn(
                name: "StudingCertificatePic",
                table: "TrainerProfile");

            migrationBuilder.DropColumn(
                name: "TrainingCertificatePic",
                table: "TrainerProfile");

            migrationBuilder.DropColumn(
                name: "WorkingCertificatePic",
                table: "TrainerProfile");
        }
    }
}
