using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class AddSPRegRequestAndSPRegResponseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SPRegRequests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RegistrationDocumentLink = table.Column<string>(type: "nvarchar(2083)", maxLength: 2083, nullable: false),
                    ServiceProviderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPRegRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SPRegRequests_ServiceProviders_ServiceProviderID",
                        column: x => x.ServiceProviderID,
                        principalTable: "ServiceProviders",
                        principalColumn: "ServiceProviderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SPRegResponses",
                columns: table => new
                {
                    ResponseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponseText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Result = table.Column<bool>(type: "bit", nullable: false),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    RespondedByID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPRegResponses", x => x.ResponseID);
                    table.ForeignKey(
                        name: "FK_SPRegResponses_Managers_RespondedByID",
                        column: x => x.RespondedByID,
                        principalTable: "Managers",
                        principalColumn: "ManagerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SPRegResponses_SPRegRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "SPRegRequests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SPRegRequests_ServiceProviderID",
                table: "SPRegRequests",
                column: "ServiceProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_SPRegResponses_RequestID",
                table: "SPRegResponses",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_SPRegResponses_RespondedByID",
                table: "SPRegResponses",
                column: "RespondedByID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SPRegResponses");

            migrationBuilder.DropTable(
                name: "SPRegRequests");
        }
    }
}
