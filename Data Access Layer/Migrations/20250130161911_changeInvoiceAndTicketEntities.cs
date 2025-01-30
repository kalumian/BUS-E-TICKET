using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class changeInvoiceAndTicketEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_InvoiceID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PaymentID",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "TicketCode",
                table: "Tickets",
                newName: "PNR");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Invoices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_InvoiceID",
                table: "Tickets",
                column: "InvoiceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentID",
                table: "Invoices",
                column: "PaymentID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_InvoiceID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PaymentID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "PNR",
                table: "Tickets",
                newName: "TicketCode");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_InvoiceID",
                table: "Tickets",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentID",
                table: "Invoices",
                column: "PaymentID");
        }
    }
}
