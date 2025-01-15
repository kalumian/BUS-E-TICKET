using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryID",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "CountryID",
                table: "Cities",
                newName: "RegionID");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_CountryID",
                table: "Cities",
                newName: "IX_Cities_RegionID");

            migrationBuilder.AddColumn<int>(
                name: "CountryEntityCountryID",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StreetID",
                table: "Addresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CityID",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LocationURL",
                table: "Addresses",
                type: "nvarchar(700)",
                maxLength: 700,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RegionEntity",
                columns: table => new
                {
                    RegionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionEntity", x => x.RegionID);
                    table.ForeignKey(
                        name: "FK_RegionEntity_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryEntityCountryID",
                table: "Cities",
                column: "CountryEntityCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityID",
                table: "Addresses",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_RegionEntity_CountryID",
                table: "RegionEntity",
                column: "CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Cities_CityID",
                table: "Addresses",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryEntityCountryID",
                table: "Cities",
                column: "CountryEntityCountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_RegionEntity_RegionID",
                table: "Cities",
                column: "RegionID",
                principalTable: "RegionEntity",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Cities_CityID",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryEntityCountryID",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_RegionEntity_RegionID",
                table: "Cities");

            migrationBuilder.DropTable(
                name: "RegionEntity");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryEntityCountryID",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CityID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CountryEntityCountryID",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CityID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LocationURL",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "RegionID",
                table: "Cities",
                newName: "CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_RegionID",
                table: "Cities",
                newName: "IX_Cities_CountryID");

            migrationBuilder.AlterColumn<int>(
                name: "StreetID",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryID",
                table: "Cities",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
