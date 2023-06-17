using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class comments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_Accounts_UserID",
                        column: x => x.UserID,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_UserId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions");

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
    }
}
