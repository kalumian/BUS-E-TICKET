using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class DeletePaymentAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayPalAccounts");

            migrationBuilder.DropTable(
                name: "PaymentAccounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentAccounts",
                columns: table => new
                {
                    PaymentAccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    ServiceProviderID = table.Column<int>(type: "int", nullable: false),
                    AccountOwnerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAccountType_ID = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAccounts", x => x.PaymentAccountID);
                    table.ForeignKey(
                        name: "FK_PaymentAccounts_Currencys_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentAccounts_ServiceProviders_ServiceProviderID",
                        column: x => x.ServiceProviderID,
                        principalTable: "ServiceProviders",
                        principalColumn: "ServiceProviderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayPalAccounts",
                columns: table => new
                {
                    PayPalAccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentAccountID = table.Column<int>(type: "int", nullable: false),
                    AccountEmail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayPalAccounts", x => x.PayPalAccountID);
                    table.ForeignKey(
                        name: "FK_PayPalAccounts_PaymentAccounts_PaymentAccountID",
                        column: x => x.PaymentAccountID,
                        principalTable: "PaymentAccounts",
                        principalColumn: "PaymentAccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAccounts_CurrencyID",
                table: "PaymentAccounts",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAccounts_ServiceProviderID",
                table: "PaymentAccounts",
                column: "ServiceProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_PayPalAccounts_PaymentAccountID",
                table: "PayPalAccounts",
                column: "PaymentAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Unique_AccountEmail",
                table: "PayPalAccounts",
                column: "AccountEmail",
                unique: true);
        }
    }
}
