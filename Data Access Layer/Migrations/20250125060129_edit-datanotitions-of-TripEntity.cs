using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class editdatanotitionsofTripEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationURL",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "LocationURL",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationURL",
                table: "Locations");

            migrationBuilder.AddColumn<string>(
                name: "LocationURL",
                table: "Addresses",
                type: "nvarchar(700)",
                maxLength: 700,
                nullable: true);
        }
    }
}
