using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaHub.Domain.Repositories;
using PizzaHub.Infrastructure.Contexts;
using PizzaHub.Infrastructure.Repositories;
using PizzaHub.Infrastructure.SendGrid;
using RecipeBook.Domain.SendGrid;

namespace PizzaHub.Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PizzaHubDbContext>(opt =>
        {
            opt.UseNpgsql(configuration["ConnectionString"]!);
        });

        services.AddScoped<IClientRepository, ClientRepositoryImp>();
        services.AddScoped<IOrderRepository, OrderRepositoryImp>();
        services.AddScoped<IItemRepository, ItemRepositoryImp>();
        services.AddScoped<ICategoryRepository, CategoryRepositoryImp>();
        services.AddScoped<IProductRepository, ProductRepositoryImp>();
        
        services.AddScoped<ISendGrid, SendGridImp>();

        return services;
    }
}