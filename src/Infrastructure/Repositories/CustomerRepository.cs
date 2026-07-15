using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Customer ccstomer)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer?> GetByCpfAsync(string cpf)
    {
        return await _context
            .Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Cpf == cpf);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context
            .Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
