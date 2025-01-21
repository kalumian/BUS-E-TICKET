using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class FixingBusinessTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SPRegRequests_Businesses_BusinessEntityBusinessID",
                table: "SPRegRequests");

            migrationBuilder.DropIndex(
                name: "IX_SPRegRequests_BusinessEntityBusinessID",
                table: "SPRegRequests");

            migrationBuilder.DropColumn(
                name: "BusinessEntityBusinessID",
                table: "SPRegRequests");

            migrationBuilder.CreateIndex(
                name: "IX_SPRegRequests_BusinessID",
                table: "SPRegRequests",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_ContactInformationID",
                table: "Businesses",
                column: "ContactInformationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_ContactInformationEntity_ContactInformationID",
                table: "Businesses",
                column: "ContactInformationID",
                principalTable: "ContactInformationEntity",
                principalColumn: "ContactInformationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SPRegRequests_Businesses_BusinessID",
                table: "SPRegRequests",
                column: "BusinessID",
                principalTable: "Businesses",
                principalColumn: "BusinessID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_ContactInformationEntity_ContactInformationID",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_SPRegRequests_Businesses_BusinessID",
                table: "SPRegRequests");

            migrationBuilder.DropIndex(
                name: "IX_SPRegRequests_BusinessID",
                table: "SPRegRequests");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_ContactInformationID",
                table: "Businesses");

            migrationBuilder.AddColumn<int>(
                name: "BusinessEntityBusinessID",
                table: "SPRegRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SPRegRequests_BusinessEntityBusinessID",
                table: "SPRegRequests",
                column: "BusinessEntityBusinessID");

            migrationBuilder.AddForeignKey(
                name: "FK_SPRegRequests_Businesses_BusinessEntityBusinessID",
                table: "SPRegRequests",
                column: "BusinessEntityBusinessID",
                principalTable: "Businesses",
                principalColumn: "BusinessID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
