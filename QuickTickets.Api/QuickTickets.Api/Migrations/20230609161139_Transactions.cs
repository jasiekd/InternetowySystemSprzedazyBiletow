using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class Transactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 9, 18, 11, 39, 349, DateTimeKind.Local).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 9, 18, 11, 39, 349, DateTimeKind.Local).AddTicks(669));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 9, 18, 11, 39, 349, DateTimeKind.Local).AddTicks(808), new DateTime(2023, 6, 9, 18, 11, 39, 349, DateTimeKind.Local).AddTicks(811) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 9, 18, 11, 39, 349, DateTimeKind.Local).AddTicks(871), new DateTime(2023, 6, 9, 18, 11, 39, 349, DateTimeKind.Local).AddTicks(874) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 9, 18, 11, 39, 349, DateTimeKind.Local).AddTicks(891), new DateTime(2023, 6, 9, 18, 11, 39, 349, DateTimeKind.Local).AddTicks(894) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 9, 18, 11, 39, 349, DateTimeKind.Local).AddTicks(907), new DateTime(2023, 6, 9, 18, 11, 39, 349, DateTimeKind.Local).AddTicks(909) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8278));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8361));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8415), new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8417) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8452), new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8454) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8464), new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8465) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8473), new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8475) });
        }
    }
}
