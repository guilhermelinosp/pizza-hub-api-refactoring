using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaHub.Domain.Entities;

namespace PizzaHub.Infrastructure.Contexts.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder
            .HasKey(u => u.AccountId)
            .HasName("PK_AccountId");
    }
}