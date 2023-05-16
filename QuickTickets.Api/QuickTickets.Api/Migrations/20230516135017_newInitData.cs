using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class newInitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("cbac6159-c495-40a0-a5c3-c07d9699aa21"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[] { new Guid("baa8ac59-26bf-4ef5-8d1b-d8ed9fb35ce7"), new DateTime(2023, 5, 16, 15, 50, 17, 279, DateTimeKind.Local).AddTicks(5734), new DateTime(2008, 3, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "Name" },
                values: new object[,]
                {
                    { 4L, "Katowice" },
                    { 5L, "Łódź" },
                    { 6L, "Gdańsk" }
                });

            migrationBuilder.InsertData(
                table: "TypesOfEvents",
                columns: new[] { "TypeID", "Description" },
                values: new object[,]
                {
                    { 4L, "Kino" },
                    { 5L, "Festiwal" },
                    { 6L, "Targi" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("baa8ac59-26bf-4ef5-8d1b-d8ed9fb35ce7"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "TypesOfEvents",
                keyColumn: "TypeID",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "TypesOfEvents",
                keyColumn: "TypeID",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "TypesOfEvents",
                keyColumn: "TypeID",
                keyValue: 6L);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[] { new Guid("cbac6159-c495-40a0-a5c3-c07d9699aa21"), new DateTime(2023, 5, 16, 14, 53, 56, 884, DateTimeKind.Local).AddTicks(1816), new DateTime(2008, 3, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" });
        }
    }
}
