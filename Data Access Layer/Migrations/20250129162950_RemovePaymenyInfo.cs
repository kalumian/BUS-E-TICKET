using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class RemovePaymenyInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentInfos_PaymentInfoID",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentInfos");

            migrationBuilder.RenameColumn(
                name: "PaymentInfoID",
                table: "Payments",
                newName: "CurrencyID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_PaymentInfoID",
                table: "Payments",
                newName: "IX_Payments_CurrencyID");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TripAmount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VAT",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Currencys_CurrencyID",
                table: "Payments",
                column: "CurrencyID",
                principalTable: "Currencys",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Currencys_CurrencyID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TripAmount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "CurrencyID",
                table: "Payments",
                newName: "PaymentInfoID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_CurrencyID",
                table: "Payments",
                newName: "IX_Payments_PaymentInfoID");

            migrationBuilder.CreateTable(
                name: "PaymentInfos",
                columns: table => new
                {
                    PaymentInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdditionalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TripAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfos", x => x.PaymentInfoID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentInfos_PaymentInfoID",
                table: "Payments",
                column: "PaymentInfoID",
                principalTable: "PaymentInfos",
                principalColumn: "PaymentInfoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
