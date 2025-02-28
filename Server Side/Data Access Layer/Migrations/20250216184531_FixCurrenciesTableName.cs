using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class FixCurrenciesTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Currencys_CurrencyID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Currencys_CurrencyID",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencys",
                table: "Currencys");

            migrationBuilder.RenameTable(
                name: "Currencys",
                newName: "Currencies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "CurrencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Currencies_CurrencyID",
                table: "Payments",
                column: "CurrencyID",
                principalTable: "Currencies",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Currencies_CurrencyID",
                table: "Trips",
                column: "CurrencyID",
                principalTable: "Currencies",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Currencies_CurrencyID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Currencies_CurrencyID",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Currencys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencys",
                table: "Currencys",
                column: "CurrencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Currencys_CurrencyID",
                table: "Payments",
                column: "CurrencyID",
                principalTable: "Currencys",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Currencys_CurrencyID",
                table: "Trips",
                column: "CurrencyID",
                principalTable: "Currencys",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
