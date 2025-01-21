using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class FixBusiness4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinessLicenseNumber",
                table: "Businesses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessLicenseNumber",
                table: "Businesses",
                column: "BusinessLicenseNumber",
                unique: true,
                filter: "[BusinessLicenseNumber] IS NOT NULL");
        }
    }
}
