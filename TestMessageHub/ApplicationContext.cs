using Microsoft.EntityFrameworkCore;
using TestMessageHub.Models;

namespace TestMessageHub
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DBMessageEntity> Messages { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=12345678");
        }
    }
}
