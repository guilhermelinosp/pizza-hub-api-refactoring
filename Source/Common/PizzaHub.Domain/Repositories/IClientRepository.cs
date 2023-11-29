#nullable enable
using PizzaHub.Domain.Entities;

namespace PizzaHub.Domain.Repositories;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(Guid id);
    Task<Client?> GetByEmailAsync(string email);
    Task<Client?> GetByPhoneAsync(string phone);
    Task<Client?> GetByCodeAsync(string code);
    Task UpdateAsync(Client client);
    Task DeleteAsync(Guid id);
    Task CreateAsync(Client client);
}