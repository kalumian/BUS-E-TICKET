using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntity_Invoices_InvoiceID",
                table: "TicketEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketEntity",
                table: "TicketEntity");

            migrationBuilder.RenameTable(
                name: "TicketEntity",
                newName: "Tickets");

            migrationBuilder.RenameIndex(
                name: "IX_TicketEntity_InvoiceID",
                table: "Tickets",
                newName: "IX_Tickets_InvoiceID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "TicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Invoices_InvoiceID",
                table: "Tickets",
                column: "InvoiceID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Invoices_InvoiceID",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "TicketEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_InvoiceID",
                table: "TicketEntity",
                newName: "IX_TicketEntity_InvoiceID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketEntity",
                table: "TicketEntity",
                column: "TicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntity_Invoices_InvoiceID",
                table: "TicketEntity",
                column: "InvoiceID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
