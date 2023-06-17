using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class deadline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeadline",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 17, 15, 38, 0, 556, DateTimeKind.Local).AddTicks(465));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 17, 15, 38, 0, 556, DateTimeKind.Local).AddTicks(547));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 17, 15, 38, 0, 556, DateTimeKind.Local).AddTicks(602), new DateTime(2023, 6, 17, 15, 38, 0, 556, DateTimeKind.Local).AddTicks(604) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 17, 15, 38, 0, 556, DateTimeKind.Local).AddTicks(644), new DateTime(2023, 6, 17, 15, 38, 0, 556, DateTimeKind.Local).AddTicks(646) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 17, 15, 38, 0, 556, DateTimeKind.Local).AddTicks(656), new DateTime(2023, 6, 17, 15, 38, 0, 556, DateTimeKind.Local).AddTicks(658) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 17, 15, 38, 0, 556, DateTimeKind.Local).AddTicks(666), new DateTime(2023, 6, 17, 15, 38, 0, 556, DateTimeKind.Local).AddTicks(667) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeadline",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 17, 0, 28, 15, 602, DateTimeKind.Local).AddTicks(8264));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 17, 0, 28, 15, 602, DateTimeKind.Local).AddTicks(8383));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 17, 0, 28, 15, 602, DateTimeKind.Local).AddTicks(8483), new DateTime(2023, 6, 17, 0, 28, 15, 602, DateTimeKind.Local).AddTicks(8487) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 17, 0, 28, 15, 602, DateTimeKind.Local).AddTicks(8529), new DateTime(2023, 6, 17, 0, 28, 15, 602, DateTimeKind.Local).AddTicks(8530) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 17, 0, 28, 15, 602, DateTimeKind.Local).AddTicks(8540), new DateTime(2023, 6, 17, 0, 28, 15, 602, DateTimeKind.Local).AddTicks(8541) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 17, 0, 28, 15, 602, DateTimeKind.Local).AddTicks(8555), new DateTime(2023, 6, 17, 0, 28, 15, 602, DateTimeKind.Local).AddTicks(8556) });
        }
    }
}
