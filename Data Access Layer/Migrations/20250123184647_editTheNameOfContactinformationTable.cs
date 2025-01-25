using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class editTheNameOfContactinformationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_ContactInformationEntity_ContactInformationID",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ContactInformationEntity_ContactInformationID",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactInformationEntity",
                table: "ContactInformationEntity");

            migrationBuilder.RenameTable(
                name: "ContactInformationEntity",
                newName: "ContactInformations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactInformations",
                table: "ContactInformations",
                column: "ContactInformationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_ContactInformations_ContactInformationID",
                table: "Businesses",
                column: "ContactInformationID",
                principalTable: "ContactInformations",
                principalColumn: "ContactInformationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_ContactInformations_ContactInformationID",
                table: "Customers",
                column: "ContactInformationID",
                principalTable: "ContactInformations",
                principalColumn: "ContactInformationID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_ContactInformations_ContactInformationID",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ContactInformations_ContactInformationID",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactInformations",
                table: "ContactInformations");

            migrationBuilder.RenameTable(
                name: "ContactInformations",
                newName: "ContactInformationEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactInformationEntity",
                table: "ContactInformationEntity",
                column: "ContactInformationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_ContactInformationEntity_ContactInformationID",
                table: "Businesses",
                column: "ContactInformationID",
                principalTable: "ContactInformationEntity",
                principalColumn: "ContactInformationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_ContactInformationEntity_ContactInformationID",
                table: "Customers",
                column: "ContactInformationID",
                principalTable: "ContactInformationEntity",
                principalColumn: "ContactInformationID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
