using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentTribe.Migrations
{
    /// <inheritdoc />
    public partial class company_injob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "companyName",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "companyName",
                table: "Jobs");
        }
    }
}
