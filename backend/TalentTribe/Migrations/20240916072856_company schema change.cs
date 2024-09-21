using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentTribe.Migrations
{
    /// <inheritdoc />
    public partial class companyschemachange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerProfiles_Companies_CompanyId",
                table: "EmployerProfiles");

            migrationBuilder.AddColumn<int>(
                name: "EmployerProfileId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_EmployerProfileId",
                table: "Companies",
                column: "EmployerProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_EmployerProfiles_EmployerProfileId",
                table: "Companies",
                column: "EmployerProfileId",
                principalTable: "EmployerProfiles",
                principalColumn: "EmployerProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerProfiles_Companies_CompanyId",
                table: "EmployerProfiles",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_EmployerProfiles_EmployerProfileId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployerProfiles_Companies_CompanyId",
                table: "EmployerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Companies_EmployerProfileId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "EmployerProfileId",
                table: "Companies");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerProfiles_Companies_CompanyId",
                table: "EmployerProfiles",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
