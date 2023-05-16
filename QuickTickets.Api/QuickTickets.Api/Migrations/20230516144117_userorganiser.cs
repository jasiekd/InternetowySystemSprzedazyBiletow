using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class userorganiser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("baa8ac59-26bf-4ef5-8d1b-d8ed9fb35ce7"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[,]
                {
                    { new Guid("2fd59429-635e-4f1e-819b-0ca2e33d23af"), new DateTime(2023, 5, 16, 16, 41, 17, 451, DateTimeKind.Local).AddTicks(6232), new DateTime(2000, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" },
                    { new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), new DateTime(2023, 5, 16, 16, 41, 17, 451, DateTimeKind.Local).AddTicks(6300), new DateTime(2002, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jkowalski01@cos.nie", null, false, "jkowalski", "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 3, "Kowalski" }
                });

            migrationBuilder.InsertData(
                table: "TypesOfEvents",
                columns: new[] { "TypeID", "Description" },
                values: new object[] { 7L, "Stand-up" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2fd59429-635e-4f1e-819b-0ca2e33d23af"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"));

            migrationBuilder.DeleteData(
                table: "TypesOfEvents",
                keyColumn: "TypeID",
                keyValue: 7L);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[] { new Guid("baa8ac59-26bf-4ef5-8d1b-d8ed9fb35ce7"), new DateTime(2023, 5, 16, 15, 50, 17, 279, DateTimeKind.Local).AddTicks(5734), new DateTime(2008, 3, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" });
        }
    }
}
