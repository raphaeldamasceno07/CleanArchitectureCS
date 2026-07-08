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

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _context
            .Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context
            .Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task UpdateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}
