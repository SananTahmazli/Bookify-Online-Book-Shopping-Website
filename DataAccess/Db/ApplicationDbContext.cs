using DataAccess.Entities.BookEntities;
using DataAccess.Entities.CartEntities;
using DataAccess.Entities.UserEntities;
using DataAccess.RoleEntities;
using Helper.EncryptionHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = 1,
                    Name = "Admin",
                    CreatedTime = DateTime.Now,
                    CreateUserId = 1
                });

            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = 2,
                    Name = "User",
                    CreatedTime = DateTime.Now,
                    CreateUserId = 1
                });

            var salt = Encryption.GenerateSalt();

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    FullName = "AdminAdmin",
                    Username = "Admin",
                    Email = "admin.admin@gmail.com",
                    BirthDate = DateTime.Now,
                    Salt = salt,
                    Hash = Encryption.GenerateHash("Admin23042002", salt),
                    RoleId = 1,
                    CreatedTime = DateTime.Now,
                    CreateUserId = 1
                });
        }
    }
}