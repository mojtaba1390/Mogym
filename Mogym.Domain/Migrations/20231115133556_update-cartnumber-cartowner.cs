using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mogym.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updatecartnumbercartowner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CartNumber",
                table: "TrainerProfile",
                type: "char(19)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(19)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CartNumber",
                table: "TrainerProfile",
                type: "char(19)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(19)",
                oldNullable: true);
        }
    }
}
