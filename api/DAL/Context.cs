using DAL.Configurations;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Database=excursionDb; Username=postgres; Password=qwer1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
