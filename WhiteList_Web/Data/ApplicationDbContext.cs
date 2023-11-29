using Microsoft.EntityFrameworkCore;
using WhiteList_Web.models;

namespace GigaChat_Bot.repositories;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=metro;Username=postgres;Password=postgres");
    }
    
    public DbSet<User> users { get; set; }
    public DbSet<History> history { get; set; }
}