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
    }
}
