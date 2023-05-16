using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class initEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2fd59429-635e-4f1e-819b-0ca2e33d23af"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[,]
                {
                    { new Guid("33daa9ec-4922-42d6-ad7a-e4ac31314f0b"), new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3464), new DateTime(2000, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" },
                    { new Guid("ccc876c5-5dbc-41f8-b0e3-574156e62a8d"), new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3531), new DateTime(2002, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jkowalski01@cos.nie", null, false, "jkowalski", "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 3, "Kowalski" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "AdultsOnly", "Date", "DateCreated", "Description", "ImgURL", "IsActive", "LocationID", "OwnerID", "Seats", "TicketPrice", "Title", "TypeID" },
                values: new object[,]
                {
                    { 1L, true, new DateTime(2023, 5, 27, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3622), "\"Ludzie trzymajcie kapelusze\" to mój drugi solowy program, grany od grudnia 2016 do sierpnia 2017 roku.  Udostępniony materiał został zarejestrowany 10 lipca 2017 roku w gdańskim klubie \"Parlament\". Obok mnie na scenie pojawił się również Adam Van Bendler.", "https://images.unsplash.com/photo-1610964199131-5e29387e6267?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1932&q=80", true, 2L, new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), 123, 10f, "Ludzie trzymajcie spodnie", 7L },
                    { 2L, false, new DateTime(2023, 5, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3644), "Spływ kajakiem po rzece Morawka", "https://images.unsplash.com/photo-1472745942893-4b9f730c7668?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1169&q=80", true, 6L, new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), 60, 25f, "Spływ kajakowy", 3L },
                    { 3L, false, new DateTime(2023, 5, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3652), "W swoim wykonaniu Pani Żak zaprezentuje swoje umiejętności artystyczne.", "https://images.unsplash.com/photo-1521116103845-2170f3377fec?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80", true, 5L, new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), 100, 15f, "Recital Pani Żak", 1L },
                    { 4L, true, new DateTime(2023, 5, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3657), "W naszej ofercie po prostu tak jakby przedstawimy oferty grona firm mówiących o swoich zapotrzebowaniach i planach dla widza.", "https://images.unsplash.com/photo-1618092388874-e262a562887f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1025&q=80", true, 1L, new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), 600, 50f, "Targi pracy", 6L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("33daa9ec-4922-42d6-ad7a-e4ac31314f0b"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("ccc876c5-5dbc-41f8-b0e3-574156e62a8d"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[,]
                {
                    { new Guid("2fd59429-635e-4f1e-819b-0ca2e33d23af"), new DateTime(2023, 5, 16, 16, 41, 17, 451, DateTimeKind.Local).AddTicks(6232), new DateTime(2000, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" },
                    { new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), new DateTime(2023, 5, 16, 16, 41, 17, 451, DateTimeKind.Local).AddTicks(6300), new DateTime(2002, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jkowalski01@cos.nie", null, false, "jkowalski", "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 3, "Kowalski" }
                });
        }
    }
}
