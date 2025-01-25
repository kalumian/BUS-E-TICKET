using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class editlocationentites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationEntityLocationEntity");

            migrationBuilder.DropColumn(
                name: "MapLocation",
                table: "Locations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MapLocation",
                table: "Locations",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_LocationEntityLocationEntity_StartLocationLocationID",
                table: "LocationEntityLocationEntity",
                column: "StartLocationLocationID");
        }
    }
}
