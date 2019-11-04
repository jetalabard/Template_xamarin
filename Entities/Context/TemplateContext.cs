using Core;
using Core.Helpers;
using Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Entities.Context
{
    public class TemplateContext : DbContext
    {
        public TemplateContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
             new Role(RoleEnum.Admin),
             new Role(RoleEnum.Default),
             new Role(RoleEnum.None));

            new HashService().CreatePasswordHash("Admin123!", out byte[] passwordHash, out byte[] passwordSalt);
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = "admin",
                LastName = "admin",
                FirstName = "admin",
                RoleId = RoleEnum.Admin.ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            });
        }
    }
}
