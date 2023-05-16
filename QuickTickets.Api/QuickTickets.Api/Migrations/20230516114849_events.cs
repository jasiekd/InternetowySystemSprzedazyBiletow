using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class events : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("e33c0333-9bed-4100-be67-846eda4de912"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Accounts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "TypesOfEvents",
                columns: table => new
                {
                    TypeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfEvents", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    TicketPrice = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AdultsOnly = table.Column<bool>(type: "bit", nullable: false),
                    TypeID = table.Column<long>(type: "bigint", nullable: false),
                    LocationID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_TypesOfEvents_TypeID",
                        column: x => x.TypeID,
                        principalTable: "TypesOfEvents",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[] { new Guid("7a8d0b4e-5d46-4e9d-ab7b-6bc665b11953"), new DateTime(2023, 5, 16, 13, 48, 49, 819, DateTimeKind.Local).AddTicks(8692), new DateTime(2008, 3, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "Name" },
                values: new object[,]
                {
                    { 1L, "Kielce" },
                    { 2L, "Kraków" },
                    { 3L, "Warszawa" }
                });

            migrationBuilder.InsertData(
                table: "TypesOfEvents",
                columns: new[] { "TypeID", "Description" },
                values: new object[,]
                {
                    { 1L, "Koncert" },
                    { 2L, "Teatr" },
                    { 3L, "Sport" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationID",
                table: "Events",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TypeID",
                table: "Events",
                column: "TypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "TypesOfEvents");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("7a8d0b4e-5d46-4e9d-ab7b-6bc665b11953"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[] { new Guid("e33c0333-9bed-4100-be67-846eda4de912"), new DateTime(2023, 5, 1, 15, 21, 53, 633, DateTimeKind.Local).AddTicks(7913), new DateTime(2008, 3, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" });
        }
    }
}
