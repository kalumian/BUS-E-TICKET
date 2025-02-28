using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class editbusinessentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_ContactInformationID",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "LogoURL",
                table: "Businesses",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_ContactInformationID",
                table: "People",
                column: "ContactInformationID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_ContactInformationID",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "LogoURL",
                table: "Businesses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_People_ContactInformationID",
                table: "People",
                column: "ContactInformationID");
        }
    }
}
