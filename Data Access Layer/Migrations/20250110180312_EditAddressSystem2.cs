using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class EditAddressSystem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryEntityCountryID",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryEntityCountryID",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CountryEntityCountryID",
                table: "Cities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryEntityCountryID",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryEntityCountryID",
                table: "Cities",
                column: "CountryEntityCountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryEntityCountryID",
                table: "Cities",
                column: "CountryEntityCountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
