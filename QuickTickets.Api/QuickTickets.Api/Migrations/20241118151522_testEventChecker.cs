using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class testEventChecker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(5979));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                column: "DateCreated",
                value: new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(6065));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(6117), new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(6118) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(6153), new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(6154) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(6163), new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(6164) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(6170), new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(6172) });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "AdultsOnly", "Date", "DateCreated", "DateModified", "Description", "ImgURL", "IsActive", "LocationID", "OwnerID", "Seats", "Status", "TicketPrice", "Title", "TypeID" },
                values: new object[] { 5L, true, new DateTime(2024, 11, 18, 16, 18, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(6178), new DateTime(2024, 11, 18, 16, 15, 22, 104, DateTimeKind.Local).AddTicks(6179), "test", "https://images.unsplash.com/photo-1618092388874-e262a562887f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1025&q=80", true, 1L, new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), 600, "Confirmed", 50f, "Targi pracy4", 6L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 5L);

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
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4739), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4774), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4776) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4783), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4784) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4791), new DateTime(2024, 11, 8, 16, 58, 4, 608, DateTimeKind.Local).AddTicks(4792) });
        }
    }
}
