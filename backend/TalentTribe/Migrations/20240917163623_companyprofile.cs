using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentTribe.Migrations
{
    /// <inheritdoc />
    public partial class companyprofile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyPictureUrl",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyPictureUrl",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyPictureUrl",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyPictureUrl",
                table: "Companies");
        }
    }
}
