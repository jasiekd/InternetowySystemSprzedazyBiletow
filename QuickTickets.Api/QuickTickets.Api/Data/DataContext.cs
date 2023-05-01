using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Entities;
using System.Security.Cryptography;
using System.Text;

namespace QuickTickets.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


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
                    DateOfBirth= new DateTime(2008, 3, 1, 7, 0, 0),
                    IsDeleted= false,
                    RoleID = 1,
                }
            );
        }
    }
}
