using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class EditServiceProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviders_Addresses_AddressID",
                table: "ServiceProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviders_ContactInformationEntity_ContactInformationID",
                table: "ServiceProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_SPRegRequests_ServiceProviders_ServiceProviderID",
                table: "SPRegRequests");

            migrationBuilder.DropIndex(
                name: "IX_SPRegRequests_ServiceProviderID",
                table: "SPRegRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProviders_AddressID",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "RegistrationDocumentLink",
                table: "SPRegRequests");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "LogoURL",
                table: "ServiceProviders");

            migrationBuilder.RenameColumn(
                name: "ServiceProviderID",
                table: "SPRegRequests",
                newName: "BusinessID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "SPRegRequests",
                newName: "SPRegRequestID");

            migrationBuilder.RenameColumn(
                name: "ContactInformationID",
                table: "ServiceProviders",
                newName: "BusinessID");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceProviders_ContactInformationID",
                table: "ServiceProviders",
                newName: "IX_ServiceProviders_BusinessID");

            migrationBuilder.AddColumn<int>(
                name: "BusinessEntityBusinessID",
                table: "SPRegRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    BusinessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LogoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    WebSiteLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessLicenseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    ContactInformationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.BusinessID);
                    table.ForeignKey(
                        name: "FK_Businesses_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SPRegRequests_BusinessEntityBusinessID",
                table: "SPRegRequests",
                column: "BusinessEntityBusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_AddressID",
                table: "Businesses",
                column: "AddressID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviders_Businesses_BusinessID",
                table: "ServiceProviders",
                column: "BusinessID",
                principalTable: "Businesses",
                principalColumn: "BusinessID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SPRegRequests_Businesses_BusinessEntityBusinessID",
                table: "SPRegRequests",
                column: "BusinessEntityBusinessID",
                principalTable: "Businesses",
                principalColumn: "BusinessID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviders_Businesses_BusinessID",
                table: "ServiceProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_SPRegRequests_Businesses_BusinessEntityBusinessID",
                table: "SPRegRequests");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_SPRegRequests_BusinessEntityBusinessID",
                table: "SPRegRequests");

            migrationBuilder.DropColumn(
                name: "BusinessEntityBusinessID",
                table: "SPRegRequests");

            migrationBuilder.RenameColumn(
                name: "BusinessID",
                table: "SPRegRequests",
                newName: "ServiceProviderID");

            migrationBuilder.RenameColumn(
                name: "SPRegRequestID",
                table: "SPRegRequests",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "BusinessID",
                table: "ServiceProviders",
                newName: "ContactInformationID");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceProviders_BusinessID",
                table: "ServiceProviders",
                newName: "IX_ServiceProviders_ContactInformationID");

            migrationBuilder.AddColumn<string>(
                name: "RegistrationDocumentLink",
                table: "SPRegRequests",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AddressID",
                table: "ServiceProviders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "ServiceProviders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogoURL",
                table: "ServiceProviders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SPRegRequests_ServiceProviderID",
                table: "SPRegRequests",
                column: "ServiceProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_AddressID",
                table: "ServiceProviders",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviders_Addresses_AddressID",
                table: "ServiceProviders",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviders_ContactInformationEntity_ContactInformationID",
                table: "ServiceProviders",
                column: "ContactInformationID",
                principalTable: "ContactInformationEntity",
                principalColumn: "ContactInformationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SPRegRequests_ServiceProviders_ServiceProviderID",
                table: "SPRegRequests",
                column: "ServiceProviderID",
                principalTable: "ServiceProviders",
                principalColumn: "ServiceProviderID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
