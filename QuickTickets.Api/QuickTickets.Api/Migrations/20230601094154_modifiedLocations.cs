using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class modifiedLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("33daa9ec-4922-42d6-ad7a-e4ac31314f0b"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("ccc876c5-5dbc-41f8-b0e3-574156e62a8d"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgURL",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[,]
                {
                    { new Guid("4761490a-a5e9-4f98-b8f5-1843c81d2b04"), new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6396), new DateTime(2002, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jkowalski01@cos.nie", null, false, "jkowalski", "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 3, "Kowalski" },
                    { new Guid("8e746219-a64c-4d88-b713-0c1b35604cbf"), new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6331), new DateTime(2000, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6436));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6455));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6462));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2023, 6, 1, 11, 41, 54, 250, DateTimeKind.Local).AddTicks(6467));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 1L,
                columns: new[] { "Description", "ImgURL" },
                values: new object[] { "Kielce to miasto położone w środkowej Polsce, stolica województwa świętokrzyskiego. Znajduje się w malowniczym regionie Gór Świętokrzyskich, co czyni je atrakcyjnym miejscem dla turystów lubiących kontakt z naturą. Miasto jest ważnym centrum gospodarczym i kulturalnym regionu. Charakterystycznymi punktami Kielc są Pałac Biskupów Krakowskich, Katedra św. Aleksandra, Zamek Królewski oraz Starówka z licznymi zabytkami. Miasto słynie także z jednej z najstarszych polskich uczelni - Politechniki Świętokrzyskiej.", "https://iili.io/Hr6wMDx.png" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 2L,
                columns: new[] { "Description", "ImgURL" },
                values: new object[] { "Kraków to jedno z najważniejszych miast Polski, położone w południowej części kraju nad rzeką Wisłą. Jest to miasto o ogromnym znaczeniu historycznym, kulturalnym i artystycznym. Stare Miasto w Krakowie zostało wpisane na Listę Światowego Dziedzictwa UNESCO i jest pełne pięknych zabytków, takich jak Wawel - zamek królewski z katedrą, Sukiennice na Rynku Głównym, Kościół Mariacki czy Barbakan. Kraków jest również ważnym ośrodkiem akademickim, z takimi instytucjami jak Uniwersytet Jagielloński, co przyciąga studentów z całego kraju i zagranicy.", "https://iili.io/Hr6whiB.png" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 3L,
                columns: new[] { "Description", "ImgURL" },
                values: new object[] { "Warszawa to stolica Polski i największe miasto w kraju, położone w centralnej części Polski nad rzeką Wisłą. Jest to dynamiczne miasto o bogatej historii i kulturze. Pomimo zniszczeń w czasie II wojny światowej, Warszawa odbudowała się i stała się nowoczesnym centrum gospodarczym i finansowym. W mieście można znaleźć wiele ważnych zabytków, takich jak Zamek Królewski, Stare Miasto, Pałac Kultury i Nauki, a także liczne muzea i teatry. Warszawa jest także siedzibą wielu instytucji państwowych i międzynarodowych.", "https://iili.io/Hr6wNl1.png" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 4L,
                columns: new[] { "Description", "ImgURL" },
                values: new object[] { "Katowice to miasto położone w południowej Polsce, w województwie śląskim. Jest to ważne centrum przemysłowe i kulturalne regionu. Katowice słyną głównie z przemysłu górniczego i hutniczego, ale w ostatnich latach stały się również miejscem wielu nowoczesnych inwestycji i wydarzeń kulturalnych. W mieście znajduje się wiele zabytków przemysłowych, takich jak kopalnie i huty, które często są przekształcane w nowoczesne centra kulturalne i biznesowe. Katowice są także siedzibą Śląskiego Uniwersytetu Medycznego oraz Filharmonii Śląskiej.", "https://iili.io/Hr6wXVV.jpg" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 5L,
                columns: new[] { "Description", "ImgURL" },
                values: new object[] { "Łódź to trzecie co do wielkości miasto w Polsce, położone w środkowej części kraju. W przeszłości było ważnym ośrodkiem tekstylnym, co nadal widać w postindustrialnym krajobrazie miasta. Łódź jest znana z bogatej historii przemysłowej i filmowej. W mieście można znaleźć wiele interesujących zabytków, takich jak Pałac Izraela Poznańskiego, dawne fabryki przekształcone w galerie sztuki czy Manufaktura - kompleks handlowo-rozrywkowy. Łódź jest również siedzibą wielu uczelni, w tym Uniwersytetu Łódzkiego.", "https://iili.io/Hr6wwKP.jpg" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 6L,
                columns: new[] { "Description", "ImgURL" },
                values: new object[] { "Gdańsk to miasto portowe położone nad Morzem Bałtyckim, na północy Polski. Jest to jedno z najważniejszych historycznych miast Polski. Gdańsk ma bogatą przeszłość jako ważny port handlowy i centrum kultury, co widać w pięknej architekturze zabytkowej. Główne atrakcje to Długi Targ z fontanną Neptuna, Bazylika Mariacka, Złota Brama oraz historyczne Stocznie Gdańskie. Miasto odgrywało również ważną rolę w historii Polski, jako miejsce, gdzie rozpoczęła się Solidarność, ruch przyczyniający się do upadku komunizmu w Europie Środkowo-Wschodniej.", "https://iili.io/Hr6wWoQ.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("4761490a-a5e9-4f98-b8f5-1843c81d2b04"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8e746219-a64c-4d88-b713-0c1b35604cbf"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ImgURL",
                table: "Locations");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[,]
                {
                    { new Guid("33daa9ec-4922-42d6-ad7a-e4ac31314f0b"), new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3464), new DateTime(2000, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" },
                    { new Guid("ccc876c5-5dbc-41f8-b0e3-574156e62a8d"), new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3531), new DateTime(2002, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jkowalski01@cos.nie", null, false, "jkowalski", "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 3, "Kowalski" }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3622));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3644));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3652));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2023, 5, 16, 16, 42, 29, 459, DateTimeKind.Local).AddTicks(3657));
        }
    }
}
