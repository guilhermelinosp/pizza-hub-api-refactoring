using Microsoft.EntityFrameworkCore;
using PizzaHub.Domain.Entities;

namespace PizzaHub.Infrastructure.Contexts.Configuration;

public class CategoryConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {

        });
    }
}