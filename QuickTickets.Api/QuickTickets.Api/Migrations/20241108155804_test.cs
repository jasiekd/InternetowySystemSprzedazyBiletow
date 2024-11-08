using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4570));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                column: "DateCreated",
                value: new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4649));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "Date", "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 30, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4739), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "Date", "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 12, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4774), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4776) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "Date", "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 12, 3, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4783), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4784) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "Date", "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 30, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4791), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4792) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(7676));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                column: "DateCreated",
                value: new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(7906));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "Date", "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 30, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8022), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8025) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "Date", "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 7, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8069), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8071) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "Date", "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 7, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8085), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8087) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "Date", "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 7, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8098), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8100) });
        }
    }
}
