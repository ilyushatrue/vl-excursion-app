using Microsoft.EntityFrameworkCore;
using DAL.Configurations;
using DAL.Models;

namespace DAL;

public class Context : DbContext
{
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql("Host=localhost; Database=excursionDb; Username=postgres; Password=qwer1234")
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
