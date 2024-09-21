using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentTribe.Migrations
{
    /// <inheritdoc />
    public partial class pp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "EmployerProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "EmployerProfiles",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
