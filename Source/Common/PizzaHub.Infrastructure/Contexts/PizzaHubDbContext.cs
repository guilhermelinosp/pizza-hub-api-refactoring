using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PizzaHub.Domain.Entities;

namespace PizzaHub.Infrastructure.Contexts;

public class PizzaHubDbContext : DbContext
{
    public PizzaHubDbContext(DbContextOptions<PizzaHubDbContext> options) : base(options)
    {
    }

    public DbSet<Client>? Accounts { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Item>? Items { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<OrderHistory>? Orders { get; set; }
    
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}