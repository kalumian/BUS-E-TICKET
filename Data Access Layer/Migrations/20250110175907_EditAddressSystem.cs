using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class EditAddressSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_RegionEntity_RegionID",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_RegionEntity_Countries_CountryID",
                table: "RegionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegionEntity",
                table: "RegionEntity");

            migrationBuilder.RenameTable(
                name: "RegionEntity",
                newName: "Regions");

            migrationBuilder.RenameIndex(
                name: "IX_RegionEntity_CountryID",
                table: "Regions",
                newName: "IX_Regions_CountryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Regions_RegionID",
                table: "Cities",
                column: "RegionID",
                principalTable: "Regions",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Countries_CountryID",
                table: "Regions",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Regions_RegionID",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Countries_CountryID",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "RegionEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Regions_CountryID",
                table: "RegionEntity",
                newName: "IX_RegionEntity_CountryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegionEntity",
                table: "RegionEntity",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_RegionEntity_RegionID",
                table: "Cities",
                column: "RegionID",
                principalTable: "RegionEntity",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegionEntity_Countries_CountryID",
                table: "RegionEntity",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
