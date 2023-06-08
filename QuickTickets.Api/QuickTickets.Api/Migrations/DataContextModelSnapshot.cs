﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickTickets.Api.Data;

#nullable disable

namespace QuickTickets.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuickTickets.Api.Entities.AccountEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GoogleSubject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"),
                            DateCreated = new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8278),
                            DateOfBirth = new DateTime(2000, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "agardian00@cos.nie",
                            IsDeleted = false,
                            Login = "agardian",
                            Name = "Artur",
                            Password = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=",
                            RoleID = 1,
                            Surname = "Gardian"
                        },
                        new
                        {
                            Id = new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                            DateCreated = new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8361),
                            DateOfBirth = new DateTime(2002, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jkowalski01@cos.nie",
                            IsDeleted = false,
                            Login = "jkowalski",
                            Name = "Jan",
                            Password = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=",
                            RoleID = 3,
                            Surname = "Kowalski"
                        });
                });

            modelBuilder.Entity("QuickTickets.Api.Entities.EventsEntity", b =>
                {
                    b.Property<long>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EventID"));

                    b.Property<bool>("AdultsOnly")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<long>("LocationID")
                        .HasColumnType("bigint");

                    b.Property<Guid>("OwnerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TicketPrice")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TypeID")
                        .HasColumnType("bigint");

                    b.HasKey("EventID");

                    b.HasIndex("LocationID");

                    b.HasIndex("OwnerID");

                    b.HasIndex("TypeID");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventID = 1L,
                            AdultsOnly = true,
                            Date = new DateTime(2023, 5, 27, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            DateCreated = new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8415),
                            DateModified = new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8417),
                            Description = "\"Ludzie trzymajcie kapelusze\" to mój drugi solowy program, grany od grudnia 2016 do sierpnia 2017 roku.  Udostępniony materiał został zarejestrowany 10 lipca 2017 roku w gdańskim klubie \"Parlament\". Obok mnie na scenie pojawił się również Adam Van Bendler.",
                            ImgURL = "https://images.unsplash.com/photo-1610964199131-5e29387e6267?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1932&q=80",
                            IsActive = true,
                            LocationID = 2L,
                            OwnerID = new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                            Seats = 123,
                            Status = "Confirmed",
                            TicketPrice = 10f,
                            Title = "Ludzie trzymajcie spodnie",
                            TypeID = 7L
                        },
                        new
                        {
                            EventID = 2L,
                            AdultsOnly = false,
                            Date = new DateTime(2023, 5, 28, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            DateCreated = new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8452),
                            DateModified = new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8454),
                            Description = "Spływ kajakiem po rzece Morawka",
                            ImgURL = "https://images.unsplash.com/photo-1472745942893-4b9f730c7668?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1169&q=80",
                            IsActive = true,
                            LocationID = 6L,
                            OwnerID = new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                            Seats = 60,
                            Status = "Confirmed",
                            TicketPrice = 25f,
                            Title = "Spływ kajakowy",
                            TypeID = 3L
                        },
                        new
                        {
                            EventID = 3L,
                            AdultsOnly = false,
                            Date = new DateTime(2023, 5, 28, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            DateCreated = new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8464),
                            DateModified = new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8465),
                            Description = "W swoim wykonaniu Pani Żak zaprezentuje swoje umiejętności artystyczne.",
                            ImgURL = "https://images.unsplash.com/photo-1521116103845-2170f3377fec?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                            IsActive = true,
                            LocationID = 5L,
                            OwnerID = new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                            Seats = 100,
                            Status = "Confirmed",
                            TicketPrice = 15f,
                            Title = "Recital Pani Żak",
                            TypeID = 1L
                        },
                        new
                        {
                            EventID = 4L,
                            AdultsOnly = true,
                            Date = new DateTime(2023, 5, 31, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            DateCreated = new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8473),
                            DateModified = new DateTime(2023, 6, 8, 12, 13, 4, 299, DateTimeKind.Local).AddTicks(8475),
                            Description = "W naszej ofercie po prostu tak jakby przedstawimy oferty grona firm mówiących o swoich zapotrzebowaniach i planach dla widza.",
                            ImgURL = "https://images.unsplash.com/photo-1618092388874-e262a562887f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1025&q=80",
                            IsActive = true,
                            LocationID = 1L,
                            OwnerID = new Guid("bb47eede-6953-43df-a26f-cdac99be8e87"),
                            Seats = 600,
                            Status = "Confirmed",
                            TicketPrice = 50f,
                            Title = "Targi pracy",
                            TypeID = 6L
                        });
                });

            modelBuilder.Entity("QuickTickets.Api.Entities.LocationsEntity", b =>
                {
                    b.Property<long>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LocationID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationID");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            LocationID = 1L,
                            Description = "Kielce to miasto położone w środkowej Polsce, stolica województwa świętokrzyskiego. Znajduje się w malowniczym regionie Gór Świętokrzyskich, co czyni je atrakcyjnym miejscem dla turystów lubiących kontakt z naturą. Miasto jest ważnym centrum gospodarczym i kulturalnym regionu. Charakterystycznymi punktami Kielc są Pałac Biskupów Krakowskich, Katedra św. Aleksandra, Zamek Królewski oraz Starówka z licznymi zabytkami. Miasto słynie także z jednej z najstarszych polskich uczelni - Politechniki Świętokrzyskiej.",
                            ImgURL = "https://iili.io/Hr6wMDx.png",
                            Name = "Kielce"
                        },
                        new
                        {
                            LocationID = 2L,
                            Description = "Kraków to jedno z najważniejszych miast Polski, położone w południowej części kraju nad rzeką Wisłą. Jest to miasto o ogromnym znaczeniu historycznym, kulturalnym i artystycznym. Stare Miasto w Krakowie zostało wpisane na Listę Światowego Dziedzictwa UNESCO i jest pełne pięknych zabytków, takich jak Wawel - zamek królewski z katedrą, Sukiennice na Rynku Głównym, Kościół Mariacki czy Barbakan. Kraków jest również ważnym ośrodkiem akademickim, z takimi instytucjami jak Uniwersytet Jagielloński, co przyciąga studentów z całego kraju i zagranicy.",
                            ImgURL = "https://iili.io/Hr6whiB.png",
                            Name = "Kraków"
                        },
                        new
                        {
                            LocationID = 3L,
                            Description = "Warszawa to stolica Polski i największe miasto w kraju, położone w centralnej części Polski nad rzeką Wisłą. Jest to dynamiczne miasto o bogatej historii i kulturze. Pomimo zniszczeń w czasie II wojny światowej, Warszawa odbudowała się i stała się nowoczesnym centrum gospodarczym i finansowym. W mieście można znaleźć wiele ważnych zabytków, takich jak Zamek Królewski, Stare Miasto, Pałac Kultury i Nauki, a także liczne muzea i teatry. Warszawa jest także siedzibą wielu instytucji państwowych i międzynarodowych.",
                            ImgURL = "https://iili.io/Hr6wNl1.png",
                            Name = "Warszawa"
                        },
                        new
                        {
                            LocationID = 4L,
                            Description = "Katowice to miasto położone w południowej Polsce, w województwie śląskim. Jest to ważne centrum przemysłowe i kulturalne regionu. Katowice słyną głównie z przemysłu górniczego i hutniczego, ale w ostatnich latach stały się również miejscem wielu nowoczesnych inwestycji i wydarzeń kulturalnych. W mieście znajduje się wiele zabytków przemysłowych, takich jak kopalnie i huty, które często są przekształcane w nowoczesne centra kulturalne i biznesowe. Katowice są także siedzibą Śląskiego Uniwersytetu Medycznego oraz Filharmonii Śląskiej.",
                            ImgURL = "https://iili.io/Hr6wXVV.jpg",
                            Name = "Katowice"
                        },
                        new
                        {
                            LocationID = 5L,
                            Description = "Łódź to trzecie co do wielkości miasto w Polsce, położone w środkowej części kraju. W przeszłości było ważnym ośrodkiem tekstylnym, co nadal widać w postindustrialnym krajobrazie miasta. Łódź jest znana z bogatej historii przemysłowej i filmowej. W mieście można znaleźć wiele interesujących zabytków, takich jak Pałac Izraela Poznańskiego, dawne fabryki przekształcone w galerie sztuki czy Manufaktura - kompleks handlowo-rozrywkowy. Łódź jest również siedzibą wielu uczelni, w tym Uniwersytetu Łódzkiego.",
                            ImgURL = "https://iili.io/Hr6wwKP.jpg",
                            Name = "Łódź"
                        },
                        new
                        {
                            LocationID = 6L,
                            Description = "Gdańsk to miasto portowe położone nad Morzem Bałtyckim, na północy Polski. Jest to jedno z najważniejszych historycznych miast Polski. Gdańsk ma bogatą przeszłość jako ważny port handlowy i centrum kultury, co widać w pięknej architekturze zabytkowej. Główne atrakcje to Długi Targ z fontanną Neptuna, Bazylika Mariacka, Złota Brama oraz historyczne Stocznie Gdańskie. Miasto odgrywało również ważną rolę w historii Polski, jako miejsce, gdzie rozpoczęła się Solidarność, ruch przyczyniający się do upadku komunizmu w Europie Środkowo-Wschodniej.",
                            ImgURL = "https://iili.io/Hr6wWoQ.jpg",
                            Name = "Gdańsk"
                        });
                });

            modelBuilder.Entity("QuickTickets.Api.Entities.OrganiserApplicationEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("OrganisersApplications");
                });

            modelBuilder.Entity("QuickTickets.Api.Entities.RoleEntity", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            Name = "admin"
                        },
                        new
                        {
                            RoleID = 2,
                            Name = "user"
                        },
                        new
                        {
                            RoleID = 3,
                            Name = "organiser"
                        });
                });

            modelBuilder.Entity("QuickTickets.Api.Entities.TypesOfEventsEntity", b =>
                {
                    b.Property<long>("TypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TypeID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeID");

                    b.ToTable("TypesOfEvents");

                    b.HasData(
                        new
                        {
                            TypeID = 1L,
                            Description = "Koncert"
                        },
                        new
                        {
                            TypeID = 2L,
                            Description = "Teatr"
                        },
                        new
                        {
                            TypeID = 3L,
                            Description = "Sport"
                        },
                        new
                        {
                            TypeID = 4L,
                            Description = "Kino"
                        },
                        new
                        {
                            TypeID = 5L,
                            Description = "Festiwal"
                        },
                        new
                        {
                            TypeID = 6L,
                            Description = "Targi"
                        },
                        new
                        {
                            TypeID = 7L,
                            Description = "Stand-up"
                        });
                });

            modelBuilder.Entity("QuickTickets.Api.Entities.EventsEntity", b =>
                {
                    b.HasOne("QuickTickets.Api.Entities.LocationsEntity", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuickTickets.Api.Entities.AccountEntity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuickTickets.Api.Entities.TypesOfEventsEntity", "Type")
                        .WithMany()
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Owner");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("QuickTickets.Api.Entities.OrganiserApplicationEntity", b =>
                {
                    b.HasOne("QuickTickets.Api.Entities.AccountEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
