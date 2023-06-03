using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Entities;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace QuickTickets.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<EventsEntity> Events { get; set; }
        public DbSet<TypesOfEventsEntity> TypesOfEvents { get; set; }
        public DbSet<LocationsEntity> Locations { get; set; } 
        public DbSet<OrganiserApplicationEntity> OrganisersApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventsEntity>()
                .HasOne(f => f.Type)
                .WithMany()
                .HasForeignKey(f => f.TypeID);

            modelBuilder.Entity<EventsEntity>()
                .HasOne(f => f.Location)
                .WithMany()
                .HasForeignKey(f => f.LocationID);

            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes("admin");
            byte[] hash = sha256.ComputeHash(bytes);
            string password = Convert.ToBase64String(hash);

            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity
                {
                    RoleID= 1,
                    Name= "admin",
                },
                new RoleEntity
                {
                    RoleID = 2,
                    Name = "user",
                },
                new RoleEntity
                {
                    RoleID = 3,
                    Name = "organiser",
                }
            );

            modelBuilder.Entity<AccountEntity>().HasData(
                new AccountEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Artur",
                    Surname = "Gardian",
                    Email = "agardian00@cos.nie",
                    Login = "agardian",
                    Password= password,
                    DateCreated= DateTime.Now,
                    DateOfBirth= DateTime.ParseExact("04-11-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    IsDeleted= false,
                    RoleID = 1,
                },
                new AccountEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Jan",
                    Surname = "Kowalski",
                    Email = "jkowalski01@cos.nie",
                    Login = "jkowalski",
                    Password = password,
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.ParseExact("11-06-2002", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    IsDeleted = false,
                    RoleID = 3,
                }
            );

            modelBuilder.Entity<LocationsEntity>().HasData(
                new LocationsEntity
                {
                    LocationID = 1,
                    Name = "Kielce",
                    Description = "Kielce to miasto położone w środkowej Polsce, stolica województwa świętokrzyskiego. Znajduje się w malowniczym regionie Gór Świętokrzyskich, co czyni je atrakcyjnym miejscem dla turystów lubiących kontakt z naturą. Miasto jest ważnym centrum gospodarczym i kulturalnym regionu. Charakterystycznymi punktami Kielc są Pałac Biskupów Krakowskich, Katedra św. Aleksandra, Zamek Królewski oraz Starówka z licznymi zabytkami. Miasto słynie także z jednej z najstarszych polskich uczelni - Politechniki Świętokrzyskiej.",
                    ImgURL = "https://iili.io/Hr6wMDx.png"
                },
                new LocationsEntity
                {
                    LocationID = 2,
                    Name = "Kraków",
                    Description = "Kraków to jedno z najważniejszych miast Polski, położone w południowej części kraju nad rzeką Wisłą. Jest to miasto o ogromnym znaczeniu historycznym, kulturalnym i artystycznym. Stare Miasto w Krakowie zostało wpisane na Listę Światowego Dziedzictwa UNESCO i jest pełne pięknych zabytków, takich jak Wawel - zamek królewski z katedrą, Sukiennice na Rynku Głównym, Kościół Mariacki czy Barbakan. Kraków jest również ważnym ośrodkiem akademickim, z takimi instytucjami jak Uniwersytet Jagielloński, co przyciąga studentów z całego kraju i zagranicy.",
                    ImgURL = "https://iili.io/Hr6whiB.png"
                },
                new LocationsEntity
                {
                    LocationID = 3,
                    Name = "Warszawa",
                    Description = "Warszawa to stolica Polski i największe miasto w kraju, położone w centralnej części Polski nad rzeką Wisłą. Jest to dynamiczne miasto o bogatej historii i kulturze. Pomimo zniszczeń w czasie II wojny światowej, Warszawa odbudowała się i stała się nowoczesnym centrum gospodarczym i finansowym. W mieście można znaleźć wiele ważnych zabytków, takich jak Zamek Królewski, Stare Miasto, Pałac Kultury i Nauki, a także liczne muzea i teatry. Warszawa jest także siedzibą wielu instytucji państwowych i międzynarodowych.",
                    ImgURL = "https://iili.io/Hr6wNl1.png"
                },
                new LocationsEntity
                {
                    LocationID = 4,
                    Name = "Katowice",
                    Description = "Katowice to miasto położone w południowej Polsce, w województwie śląskim. Jest to ważne centrum przemysłowe i kulturalne regionu. Katowice słyną głównie z przemysłu górniczego i hutniczego, ale w ostatnich latach stały się również miejscem wielu nowoczesnych inwestycji i wydarzeń kulturalnych. W mieście znajduje się wiele zabytków przemysłowych, takich jak kopalnie i huty, które często są przekształcane w nowoczesne centra kulturalne i biznesowe. Katowice są także siedzibą Śląskiego Uniwersytetu Medycznego oraz Filharmonii Śląskiej.",
                    ImgURL = "https://iili.io/Hr6wXVV.jpg"
                },
                new LocationsEntity
                {
                    LocationID = 5,
                    Name = "Łódź",
                    Description = "Łódź to trzecie co do wielkości miasto w Polsce, położone w środkowej części kraju. W przeszłości było ważnym ośrodkiem tekstylnym, co nadal widać w postindustrialnym krajobrazie miasta. Łódź jest znana z bogatej historii przemysłowej i filmowej. W mieście można znaleźć wiele interesujących zabytków, takich jak Pałac Izraela Poznańskiego, dawne fabryki przekształcone w galerie sztuki czy Manufaktura - kompleks handlowo-rozrywkowy. Łódź jest również siedzibą wielu uczelni, w tym Uniwersytetu Łódzkiego.",
                    ImgURL = "https://iili.io/Hr6wwKP.jpg"
                },
                new LocationsEntity
                {
                    LocationID = 6,
                    Name = "Gdańsk",
                    Description = "Gdańsk to miasto portowe położone nad Morzem Bałtyckim, na północy Polski. Jest to jedno z najważniejszych historycznych miast Polski. Gdańsk ma bogatą przeszłość jako ważny port handlowy i centrum kultury, co widać w pięknej architekturze zabytkowej. Główne atrakcje to Długi Targ z fontanną Neptuna, Bazylika Mariacka, Złota Brama oraz historyczne Stocznie Gdańskie. Miasto odgrywało również ważną rolę w historii Polski, jako miejsce, gdzie rozpoczęła się Solidarność, ruch przyczyniający się do upadku komunizmu w Europie Środkowo-Wschodniej.",
                    ImgURL = "https://iili.io/Hr6wWoQ.jpg"
                }
            );

            modelBuilder.Entity<TypesOfEventsEntity>().HasData(
                new TypesOfEventsEntity
                {
                    TypeID = 1,
                    Description = "Koncert",
                },
                new TypesOfEventsEntity
                {
                    TypeID = 2,
                    Description = "Teatr",
                },
                new TypesOfEventsEntity
                {
                    TypeID = 3,
                    Description = "Sport",
                },
                new TypesOfEventsEntity
                {
                    TypeID = 4,
                    Description = "Kino",
                },
                new TypesOfEventsEntity
                {
                    TypeID = 5,
                    Description = "Festiwal",
                },
                new TypesOfEventsEntity
                {
                    TypeID = 6,
                    Description = "Targi",
                },
                new TypesOfEventsEntity
                {
                    TypeID = 7,
                    Description = "Stand-up",
                }
            );

            modelBuilder.Entity<EventsEntity>().HasData(
                new EventsEntity
                {
                    EventID = 1,
                    Title = "Ludzie trzymajcie spodnie",
                    Seats = 123,
                    TicketPrice = 10,
                    Description = "\"Ludzie trzymajcie kapelusze\" to mój drugi solowy program, grany od grudnia 2016 do sierpnia 2017 roku.  Udostępniony materiał został zarejestrowany 10 lipca 2017 roku w gdańskim klubie \"Parlament\". Obok mnie na scenie pojawił się również Adam Van Bendler.",
                    Date = DateTime.ParseExact("27-05-2023 18:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    Status = StatusEnum.Confirmed.ToString(),
                    IsActive = true,
                    AdultsOnly = true,
                    TypeID = 7,
                    LocationID = 2,
                    ImgURL = "https://images.unsplash.com/photo-1610964199131-5e29387e6267?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1932&q=80",
                    OwnerID = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87")
                },
                new EventsEntity
                {
                    EventID = 2,
                    Title = "Spływ kajakowy",
                    Seats = 60,
                    TicketPrice = 25,
                    Description = "Spływ kajakiem po rzece Morawka",
                    Date = DateTime.ParseExact("28-05-2023 12:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    Status = StatusEnum.Confirmed.ToString(),
                    IsActive = true,
                    AdultsOnly = false,
                    TypeID = 3,
                    LocationID = 6,
                    ImgURL = "https://images.unsplash.com/photo-1472745942893-4b9f730c7668?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1169&q=80",
                    OwnerID = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87")
                }
                ,
                new EventsEntity
                {
                    EventID = 3,
                    Title = "Recital Pani Żak",
                    Seats = 100,
                    TicketPrice = 15,
                    Description = "W swoim wykonaniu Pani Żak zaprezentuje swoje umiejętności artystyczne.",
                    Date = DateTime.ParseExact("28-05-2023 18:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    Status = StatusEnum.Confirmed.ToString(),
                    IsActive = true,
                    AdultsOnly = false,
                    TypeID = 1,
                    LocationID = 5,
                    ImgURL = "https://images.unsplash.com/photo-1521116103845-2170f3377fec?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                    OwnerID = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87")
                }
                ,
                new EventsEntity
                {
                    EventID = 4,
                    Title = "Targi pracy",
                    Seats = 600,
                    TicketPrice = 50,
                    Description = "W naszej ofercie po prostu tak jakby przedstawimy oferty grona firm mówiących o swoich zapotrzebowaniach i planach dla widza.",
                    Date = DateTime.ParseExact("31-05-2023 12:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    Status = StatusEnum.Confirmed.ToString(),
                    IsActive = true,
                    AdultsOnly = true,
                    TypeID = 6,
                    LocationID = 1,
                    ImgURL = "https://images.unsplash.com/photo-1618092388874-e262a562887f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1025&q=80",
                    OwnerID = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87")
                }
                ) ;
        }
    }
}
