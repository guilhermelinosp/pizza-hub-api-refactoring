using Microsoft.EntityFrameworkCore;
using PizzaHub.Domain.Entities;
using PizzaHub.Domain.Repositories;
using PizzaHub.Infrastructure.Contexts;

namespace PizzaHub.Infrastructure.Repositories;

public class ClientRepositoryImp : IClientRepository
{
    private readonly PizzaHubDbContext _context;

    public ClientRepositoryImp(PizzaHubDbContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetByIdAsync(Guid id)
    {
        return await _context.Accounts!.AsNoTracking().FirstOrDefaultAsync(u => u.AccountId == id);
    }

    public async Task<Client?> GetByEmailAsync(string email)
    {
        return await _context.Accounts!.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Client?> GetByPhoneAsync(string phone)
    {
        return await _context.Accounts!.AsNoTracking().FirstOrDefaultAsync(u => u.Phone == phone);
    }

    public async Task<Client?> GetByCodeAsync(string code)
    {
        return await _context.Accounts!.AsNoTracking().FirstOrDefaultAsync(u => u.Code == code);
    }

    public async Task CreateAsync(Client client)
    {
        await _context.Accounts!.AddAsync(client);

        await SaveChangesAsync();
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Accounts!.Update(client);

        await SaveChangesAsync();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Accounts!.AsNoTracking().ToListAsync();
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}