using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class asasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ContactInformations_ContactInformationID",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ContactInformationID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ContactInformationID",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "ContactInformationID",
                table: "People",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_People_ContactInformationID",
                table: "People",
                column: "ContactInformationID");

            migrationBuilder.AddForeignKey(
                name: "FK_People_ContactInformations_ContactInformationID",
                table: "People",
                column: "ContactInformationID",
                principalTable: "ContactInformations",
                principalColumn: "ContactInformationID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_ContactInformations_ContactInformationID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_ContactInformationID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "ContactInformationID",
                table: "People");

            migrationBuilder.AddColumn<int>(
                name: "ContactInformationID",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ContactInformationID",
                table: "Customers",
                column: "ContactInformationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_ContactInformations_ContactInformationID",
                table: "Customers",
                column: "ContactInformationID",
                principalTable: "ContactInformations",
                principalColumn: "ContactInformationID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
