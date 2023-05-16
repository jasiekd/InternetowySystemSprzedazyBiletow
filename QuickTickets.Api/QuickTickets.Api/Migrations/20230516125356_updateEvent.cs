using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class updateEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("7a8d0b4e-5d46-4e9d-ab7b-6bc665b11953"));

            migrationBuilder.AddColumn<string>(
                name: "ImgURL",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerID",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[] { new Guid("cbac6159-c495-40a0-a5c3-c07d9699aa21"), new DateTime(2023, 5, 16, 14, 53, 56, 884, DateTimeKind.Local).AddTicks(1816), new DateTime(2008, 3, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("cbac6159-c495-40a0-a5c3-c07d9699aa21"));

            migrationBuilder.DropColumn(
                name: "ImgURL",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Events");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[] { new Guid("7a8d0b4e-5d46-4e9d-ab7b-6bc665b11953"), new DateTime(2023, 5, 16, 13, 48, 49, 819, DateTimeKind.Local).AddTicks(8692), new DateTime(2008, 3, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" });
        }
    }
}
