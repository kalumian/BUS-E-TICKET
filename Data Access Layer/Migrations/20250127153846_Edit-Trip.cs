using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class EditTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedSeatsBinary",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "AvailableSeatsCount",
                table: "Trips",
                newName: "VehicleCapacity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VehicleCapacity",
                table: "Trips",
                newName: "AvailableSeatsCount");

            migrationBuilder.AddColumn<byte[]>(
                name: "ReservedSeatsBinary",
                table: "Trips",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
