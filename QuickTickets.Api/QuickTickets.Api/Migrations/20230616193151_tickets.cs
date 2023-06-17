using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class tickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transactions",
                newName: "TransactionID");

            migrationBuilder.AddColumn<string>(
                name: "DotPayID",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 16, 21, 31, 51, 631, DateTimeKind.Local).AddTicks(7763));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 16, 21, 31, 51, 631, DateTimeKind.Local).AddTicks(7851));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 31, 51, 631, DateTimeKind.Local).AddTicks(7906), new DateTime(2023, 6, 16, 21, 31, 51, 631, DateTimeKind.Local).AddTicks(7909) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 31, 51, 631, DateTimeKind.Local).AddTicks(7948), new DateTime(2023, 6, 16, 21, 31, 51, 631, DateTimeKind.Local).AddTicks(7949) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 31, 51, 631, DateTimeKind.Local).AddTicks(7959), new DateTime(2023, 6, 16, 21, 31, 51, 631, DateTimeKind.Local).AddTicks(7961) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 21, 31, 51, 631, DateTimeKind.Local).AddTicks(7970), new DateTime(2023, 6, 16, 21, 31, 51, 631, DateTimeKind.Local).AddTicks(7971) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DotPayID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "Transactions",
                newName: "Id");

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
    }
}
