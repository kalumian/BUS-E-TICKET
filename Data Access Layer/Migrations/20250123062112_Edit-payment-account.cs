using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class Editpaymentaccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayPalAccounts_Countries_CountryID",
                table: "PayPalAccounts");

            migrationBuilder.DropIndex(
                name: "IX_SPRegResponses_RequestID",
                table: "SPRegResponses");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProviders_BusinessID",
                table: "ServiceProviders");

            migrationBuilder.DropIndex(
                name: "IX_PayPalAccounts_CountryID",
                table: "PayPalAccounts");

            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "PayPalAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_SPRegResponses_RequestID",
                table: "SPRegResponses",
                column: "RequestID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_BusinessID",
                table: "ServiceProviders",
                column: "BusinessID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SPRegResponses_RequestID",
                table: "SPRegResponses");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProviders_BusinessID",
                table: "ServiceProviders");

            migrationBuilder.AddColumn<int>(
                name: "CountryID",
                table: "PayPalAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SPRegResponses_RequestID",
                table: "SPRegResponses",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_BusinessID",
                table: "ServiceProviders",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_PayPalAccounts_CountryID",
                table: "PayPalAccounts",
                column: "CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_PayPalAccounts_Countries_CountryID",
                table: "PayPalAccounts",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
