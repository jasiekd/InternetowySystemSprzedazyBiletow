using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class firstAttemptForRecommendation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModelID",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserEventHistory",
                columns: table => new
                {
                    UserEventHistoryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    EventID = table.Column<long>(type: "bigint", nullable: false),
                    Label = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEventHistory", x => x.UserEventHistoryID);
                });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                columns: new[] { "DateCreated", "ModelID" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(7676), "041120001" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                columns: new[] { "DateCreated", "ModelID" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(7906), "110620021" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8022), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8025) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8069), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8071) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8085), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8087) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8098), new DateTime(2024, 11, 8, 16, 8, 20, 478, DateTimeKind.Local).AddTicks(8100) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEventHistory");

            migrationBuilder.DropColumn(
                name: "ModelID",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(4510));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(4694));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(4880), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(4884) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(4989), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(5005) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(5043), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(5046) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(5058), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(5061) });
        }
    }
}
