using Microsoft.EntityFrameworkCore;
using GigaChat_Bot.models;

namespace GigaChat_Bot.repositories;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // For Docker
        // optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=GigaChatDb;Username=postgres;Password=postgres");
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=GigaChatDb;Username=postgres;Password=postgres");
    }
    
    public DbSet<User> users { get; set; }
    public DbSet<History> history { get; set; }
}