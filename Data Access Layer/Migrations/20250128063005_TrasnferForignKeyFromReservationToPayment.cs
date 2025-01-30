using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class TrasnferForignKeyFromReservationToPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Payments_PaymentID",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "Reservations",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_PaymentID",
                table: "Reservations",
                newName: "IX_Reservations_CustomerID");

            migrationBuilder.AddColumn<int>(
                name: "ReservationID",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ReservationID",
                table: "Payments",
                column: "ReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Reservations_ReservationID",
                table: "Payments",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "ReservationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Customers_CustomerID",
                table: "Reservations",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Reservations_ReservationID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Customers_CustomerID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ReservationID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ReservationID",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Reservations",
                newName: "PaymentID");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_CustomerID",
                table: "Reservations",
                newName: "IX_Reservations_PaymentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Payments_PaymentID",
                table: "Reservations",
                column: "PaymentID",
                principalTable: "Payments",
                principalColumn: "PaymentID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
