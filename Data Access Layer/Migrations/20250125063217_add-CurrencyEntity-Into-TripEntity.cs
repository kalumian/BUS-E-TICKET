using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class addCurrencyEntityIntoTripEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyID",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CurrencyID",
                table: "Trips",
                column: "CurrencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Currencys_CurrencyID",
                table: "Trips",
                column: "CurrencyID",
                principalTable: "Currencys",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Currencys_CurrencyID",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_CurrencyID",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CurrencyID",
                table: "Trips");
        }
    }
}
