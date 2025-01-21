using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class FixBusiness3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Businesses_AddressID",
                table: "Businesses");

            migrationBuilder.AddColumn<int>(
                name: "BusinessID",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_AddressID",
                table: "Businesses",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessLicenseNumber",
                table: "Businesses",
                column: "BusinessLicenseNumber",
                unique: true,
                filter: "[BusinessLicenseNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_BusinessID",
                table: "Addresses",
                column: "BusinessID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Businesses_BusinessID",
                table: "Addresses",
                column: "BusinessID",
                principalTable: "Businesses",
                principalColumn: "BusinessID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Businesses_BusinessID",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_AddressID",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinessLicenseNumber",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_BusinessID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "BusinessID",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_AddressID",
                table: "Businesses",
                column: "AddressID",
                unique: true);
        }
    }
}
