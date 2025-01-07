using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class AddTripsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationEntityLocationEntity",
                columns: table => new
                {
                    EndLocationLocationID = table.Column<int>(type: "int", nullable: false),
                    StartLocationLocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationEntityLocationEntity", x => new { x.EndLocationLocationID, x.StartLocationLocationID });
                    table.ForeignKey(
                        name: "FK_LocationEntityLocationEntity_Locations_EndLocationLocationID",
                        column: x => x.EndLocationLocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationEntityLocationEntity_Locations_StartLocationLocationID",
                        column: x => x.StartLocationLocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleInfo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DriverInfo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalSeats = table.Column<short>(type: "smallint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TripStatus = table.Column<int>(type: "int", nullable: false),
                    EstimatedArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TripDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    AvailableSeatsCount = table.Column<short>(type: "smallint", nullable: false),
                    ReservedSeatsBinary = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ServiceProviderID = table.Column<int>(type: "int", nullable: false),
                    StartLocationID = table.Column<int>(type: "int", nullable: false),
                    EndLocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripID);
                    table.ForeignKey(
                        name: "FK_Trips_Locations_EndLocationID",
                        column: x => x.EndLocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Locations_StartLocationID",
                        column: x => x.StartLocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_ServiceProviders_ServiceProviderID",
                        column: x => x.ServiceProviderID,
                        principalTable: "ServiceProviders",
                        principalColumn: "ServiceProviderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationEntityLocationEntity_StartLocationLocationID",
                table: "LocationEntityLocationEntity",
                column: "StartLocationLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_EndLocationID",
                table: "Trips",
                column: "EndLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ServiceProviderID",
                table: "Trips",
                column: "ServiceProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_StartLocationID",
                table: "Trips",
                column: "StartLocationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationEntityLocationEntity");

            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
