using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class OrganiserApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("4761490a-a5e9-4f98-b8f5-1843c81d2b04"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8e746219-a64c-4d88-b713-0c1b35604cbf"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OrganisersApplications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisersApplications", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[,]
                {
                    { new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"), new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2224), new DateTime(2000, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" },
                    { new Guid("c5f2a066-77b9-4d29-b6b4-a66ec3a7644f"), new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2484), new DateTime(2002, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jkowalski01@cos.nie", null, false, "jkowalski", "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 3, "Kowalski" }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified", "Status" },
                values: new object[] { new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2569), new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2571), "Confirmed" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified", "Status" },
                values: new object[] { new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2711), new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2712), "Confirmed" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified", "Status" },
                values: new object[] { new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2727), new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2729), "Confirmed" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified", "Status" },
                values: new object[] { new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2737), new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2738), "Confirmed" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganisersApplications");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c5f2a066-77b9-4d29-b6b4-a66ec3a7644f"));

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Events");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[,]
                {
                    { new Guid("4761490a-a5e9-4f98-b8f5-1843c81d2b04"), new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6396), new DateTime(2002, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jkowalski01@cos.nie", null, false, "jkowalski", "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 3, "Kowalski" },
                    { new Guid("8e746219-a64c-4d88-b713-0c1b35604cbf"), new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6331), new DateTime(2000, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6436));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6455));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6462));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6467));
        }
    }
}
