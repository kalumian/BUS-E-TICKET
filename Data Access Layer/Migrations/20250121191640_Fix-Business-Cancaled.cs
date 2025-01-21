using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class FixBusinessCancaled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Businesses_BusinessID",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_AddressID",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_BusinessID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "BusinessID",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_AddressID",
                table: "Businesses",
                column: "AddressID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Businesses_AddressID",
                table: "Businesses");

            migrationBuilder.AddColumn<int>(
                name: "BusinessID",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_AddressID",
                table: "Businesses",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_BusinessID",
                table: "Addresses",
                column: "BusinessID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Businesses_BusinessID",
                table: "Addresses",
                column: "BusinessID",
                principalTable: "Businesses",
                principalColumn: "BusinessID");
        }
    }
}
