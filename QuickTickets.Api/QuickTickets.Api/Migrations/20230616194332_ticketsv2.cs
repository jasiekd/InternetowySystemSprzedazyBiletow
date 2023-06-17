using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class ticketsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EventID = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TransactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_Tickets_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Transactions_TransactionID",
                        column: x => x.TransactionID,
                        principalTable: "Transactions",
                        principalColumn: "TransactionID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventID",
                table: "Tickets",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TransactionID",
                table: "Tickets",
                column: "TransactionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

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
    }
}
