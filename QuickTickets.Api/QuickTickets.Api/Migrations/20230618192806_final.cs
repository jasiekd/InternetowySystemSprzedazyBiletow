using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuickTickets.Api.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    GoogleSubject = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "TypesOfEvents",
                columns: table => new
                {
                    TypeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfEvents", x => x.TypeID);
                });

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

            migrationBuilder.CreateTable(
                name: "OrganisersApplications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisersApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganisersApplications_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DotPayID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    TicketPrice = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AdultsOnly = table.Column<bool>(type: "bit", nullable: false),
                    TypeID = table.Column<long>(type: "bigint", nullable: false),
                    LocationID = table.Column<long>(type: "bigint", nullable: false),
                    ImgURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_Accounts_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_TypesOfEvents_TypeID",
                        column: x => x.TypeID,
                        principalTable: "TypesOfEvents",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EventID = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "DateCreated", "DateOfBirth", "Email", "GoogleSubject", "IsDeleted", "Login", "Name", "Password", "RoleID", "Surname" },
                values: new object[,]
                {
                    { new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(4510), new DateTime(2000, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "agardian00@cos.nie", null, false, "agardian", "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Gardian" },
                    { new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(4694), new DateTime(2002, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jkowalski01@cos.nie", null, false, "jkowalski", "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 3, "Kowalski" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "Description", "ImgURL", "Name" },
                values: new object[,]
                {
                    { 1L, "Kielce to miasto położone w środkowej Polsce, stolica województwa świętokrzyskiego. Znajduje się w malowniczym regionie Gór Świętokrzyskich, co czyni je atrakcyjnym miejscem dla turystów lubiących kontakt z naturą. Miasto jest ważnym centrum gospodarczym i kulturalnym regionu. Charakterystycznymi punktami Kielc są Pałac Biskupów Krakowskich, Katedra św. Aleksandra, Zamek Królewski oraz Starówka z licznymi zabytkami. Miasto słynie także z jednej z najstarszych polskich uczelni - Politechniki Świętokrzyskiej.", "https://iili.io/Hr6wMDx.png", "Kielce" },
                    { 2L, "Kraków to jedno z najważniejszych miast Polski, położone w południowej części kraju nad rzeką Wisłą. Jest to miasto o ogromnym znaczeniu historycznym, kulturalnym i artystycznym. Stare Miasto w Krakowie zostało wpisane na Listę Światowego Dziedzictwa UNESCO i jest pełne pięknych zabytków, takich jak Wawel - zamek królewski z katedrą, Sukiennice na Rynku Głównym, Kościół Mariacki czy Barbakan. Kraków jest również ważnym ośrodkiem akademickim, z takimi instytucjami jak Uniwersytet Jagielloński, co przyciąga studentów z całego kraju i zagranicy.", "https://iili.io/Hr6whiB.png", "Kraków" },
                    { 3L, "Warszawa to stolica Polski i największe miasto w kraju, położone w centralnej części Polski nad rzeką Wisłą. Jest to dynamiczne miasto o bogatej historii i kulturze. Pomimo zniszczeń w czasie II wojny światowej, Warszawa odbudowała się i stała się nowoczesnym centrum gospodarczym i finansowym. W mieście można znaleźć wiele ważnych zabytków, takich jak Zamek Królewski, Stare Miasto, Pałac Kultury i Nauki, a także liczne muzea i teatry. Warszawa jest także siedzibą wielu instytucji państwowych i międzynarodowych.", "https://iili.io/Hr6wNl1.png", "Warszawa" },
                    { 4L, "Katowice to miasto położone w południowej Polsce, w województwie śląskim. Jest to ważne centrum przemysłowe i kulturalne regionu. Katowice słyną głównie z przemysłu górniczego i hutniczego, ale w ostatnich latach stały się również miejscem wielu nowoczesnych inwestycji i wydarzeń kulturalnych. W mieście znajduje się wiele zabytków przemysłowych, takich jak kopalnie i huty, które często są przekształcane w nowoczesne centra kulturalne i biznesowe. Katowice są także siedzibą Śląskiego Uniwersytetu Medycznego oraz Filharmonii Śląskiej.", "https://iili.io/Hr6wXVV.jpg", "Katowice" },
                    { 5L, "Łódź to trzecie co do wielkości miasto w Polsce, położone w środkowej części kraju. W przeszłości było ważnym ośrodkiem tekstylnym, co nadal widać w postindustrialnym krajobrazie miasta. Łódź jest znana z bogatej historii przemysłowej i filmowej. W mieście można znaleźć wiele interesujących zabytków, takich jak Pałac Izraela Poznańskiego, dawne fabryki przekształcone w galerie sztuki czy Manufaktura - kompleks handlowo-rozrywkowy. Łódź jest również siedzibą wielu uczelni, w tym Uniwersytetu Łódzkiego.", "https://iili.io/Hr6wwKP.jpg", "Łódź" },
                    { 6L, "Gdańsk to miasto portowe położone nad Morzem Bałtyckim, na północy Polski. Jest to jedno z najważniejszych historycznych miast Polski. Gdańsk ma bogatą przeszłość jako ważny port handlowy i centrum kultury, co widać w pięknej architekturze zabytkowej. Główne atrakcje to Długi Targ z fontanną Neptuna, Bazylika Mariacka, Złota Brama oraz historyczne Stocznie Gdańskie. Miasto odgrywało również ważną rolę w historii Polski, jako miejsce, gdzie rozpoczęła się Solidarność, ruch przyczyniający się do upadku komunizmu w Europie Środkowo-Wschodniej.", "https://iili.io/Hr6wWoQ.jpg", "Gdańsk" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Name" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "user" },
                    { 3, "organiser" }
                });

            migrationBuilder.InsertData(
                table: "TypesOfEvents",
                columns: new[] { "TypeID", "Description" },
                values: new object[,]
                {
                    { 1L, "Koncert" },
                    { 2L, "Teatr" },
                    { 3L, "Sport" },
                    { 4L, "Kino" },
                    { 5L, "Festiwal" },
                    { 6L, "Targi" },
                    { 7L, "Stand-up" },
                    { 8L, "Dla dzieci" },
                    { 9L, "Klasyka" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "AdultsOnly", "Date", "DateCreated", "DateModified", "Description", "ImgURL", "IsActive", "LocationID", "OwnerID", "Seats", "Status", "TicketPrice", "Title", "TypeID" },
                values: new object[,]
                {
                    { 1L, true, new DateTime(2023, 6, 30, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(4880), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(4884), "\"Ludzie trzymajcie kapelusze\" to mój drugi solowy program, grany od grudnia 2016 do sierpnia 2017 roku.  Udostępniony materiał został zarejestrowany 10 lipca 2017 roku w gdańskim klubie \"Parlament\". Obok mnie na scenie pojawił się również Adam Van Bendler.", "https://images.unsplash.com/photo-1610964199131-5e29387e6267?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1932&q=80", true, 2L, new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), 123, "Confirmed", 10f, "Ludzie trzymajcie spodnie", 7L },
                    { 2L, false, new DateTime(2023, 7, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(4989), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(5005), "Spływ kajakiem po rzece Morawka", "https://images.unsplash.com/photo-1472745942893-4b9f730c7668?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1169&q=80", true, 6L, new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), 60, "Confirmed", 25f, "Spływ kajakowy", 3L },
                    { 3L, false, new DateTime(2023, 7, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(5043), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(5046), "W swoim wykonaniu Pani Żak zaprezentuje swoje umiejętności artystyczne.", "https://images.unsplash.com/photo-1521116103845-2170f3377fec?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80", true, 5L, new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), 100, "Confirmed", 15f, "Recital Pani Żak", 1L },
                    { 4L, true, new DateTime(2023, 7, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(5058), new DateTime(2023, 6, 18, 21, 28, 5, 927, DateTimeKind.Local).AddTicks(5061), "W naszej ofercie po prostu tak jakby przedstawimy oferty grona firm mówiących o swoich zapotrzebowaniach i planach dla widza.", "https://images.unsplash.com/photo-1618092388874-e262a562887f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1025&q=80", true, 1L, new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"), 600, "Confirmed", 50f, "Targi pracy", 6L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationID",
                table: "Events",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OwnerID",
                table: "Events",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TypeID",
                table: "Events",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisersApplications_UserId",
                table: "OrganisersApplications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventID",
                table: "Tickets",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TransactionID",
                table: "Tickets",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "OrganisersApplications");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "TypesOfEvents");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
