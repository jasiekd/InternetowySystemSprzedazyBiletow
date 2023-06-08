using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class DataContextChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c5f2a066-77b9-4d29-b6b4-a66ec3a7644f"));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8278));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[] { new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8361), new DateTime(2002, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jkowalski01@cos.nie", null, false, "jkowalski", "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 3, "Kowalski" });

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

            migrationBuilder.CreateIndex(
                name: "IX_OrganisersApplications_UserId",
                table: "OrganisersApplications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OwnerID",
                table: "Events",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Accounts_OwnerID",
                table: "Events",
                column: "OwnerID",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisersApplications_Accounts_UserId",
                table: "OrganisersApplications",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Accounts_OwnerID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisersApplications_Accounts_UserId",
                table: "OrganisersApplications");

            migrationBuilder.DropIndex(
                name: "IX_OrganisersApplications_UserId",
                table: "OrganisersApplications");

            migrationBuilder.DropIndex(
                name: "IX_Events_OwnerID",
                table: "Events");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                column: "DateCreated",
                value: new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2224));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[] { new Guid("c5f2a066-77b9-4d29-b6b4-a66ec3a7644f"), new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2484), new DateTime(2002, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jkowalski01@cos.nie", null, false, "jkowalski", "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 3, "Kowalski" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2569), new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2571) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2711), new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2712) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2727), new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2729) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2737), new DateTime(2023, 6, 3, 13, 36, 30, 763, DateTimeKind.Local).AddTicks(2738) });
        }
    }
}
