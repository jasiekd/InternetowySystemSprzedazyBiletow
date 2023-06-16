using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class ticketsv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 16, 21, 45, 36, 12, DateTimeKind.Local).AddTicks(7252));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 16, 21, 45, 36, 12, DateTimeKind.Local).AddTicks(7344));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 45, 36, 12, DateTimeKind.Local).AddTicks(7403), new DateTime(2023, 6, 16, 21, 45, 36, 12, DateTimeKind.Local).AddTicks(7405) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 45, 36, 12, DateTimeKind.Local).AddTicks(7443), new DateTime(2023, 6, 16, 21, 45, 36, 12, DateTimeKind.Local).AddTicks(7445) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 45, 36, 12, DateTimeKind.Local).AddTicks(7454), new DateTime(2023, 6, 16, 21, 45, 36, 12, DateTimeKind.Local).AddTicks(7456) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 45, 36, 12, DateTimeKind.Local).AddTicks(7464), new DateTime(2023, 6, 16, 21, 45, 36, 12, DateTimeKind.Local).AddTicks(7465) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 16, 21, 43, 32, 837, DateTimeKind.Local).AddTicks(2904));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 16, 21, 43, 32, 837, DateTimeKind.Local).AddTicks(2991));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 43, 32, 837, DateTimeKind.Local).AddTicks(3046), new DateTime(2023, 6, 16, 21, 43, 32, 837, DateTimeKind.Local).AddTicks(3047) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 43, 32, 837, DateTimeKind.Local).AddTicks(3083), new DateTime(2023, 6, 16, 21, 43, 32, 837, DateTimeKind.Local).AddTicks(3085) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 43, 32, 837, DateTimeKind.Local).AddTicks(3096), new DateTime(2023, 6, 16, 21, 43, 32, 837, DateTimeKind.Local).AddTicks(3097) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 43, 32, 837, DateTimeKind.Local).AddTicks(3105), new DateTime(2023, 6, 16, 21, 43, 32, 837, DateTimeKind.Local).AddTicks(3106) });
        }
    }
}
