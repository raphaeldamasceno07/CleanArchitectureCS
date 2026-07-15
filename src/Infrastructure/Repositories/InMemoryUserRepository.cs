using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Concurrent;

namespace Infrastructure.Repositories;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly ConcurrentDictionary<Guid, Customer> _customers = new();

    public Task AddAsync(Customer customer)
    {
        _customers[customer.Id] = customer;
        return Task.CompletedTask;
    }

    public Task<Customer?> GetByCpfAsync(string cpf)
    {
        var customer = _customers.Values.FirstOrDefault(c => c.Cpf.Equals(cpf, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult<Customer?>(customer);
    }

    public Task<Customer?> GetByIdAsync(Guid id)
    {
        _customers.TryGetValue(id, out var customer);
        return Task.FromResult<Customer?>(customer);
    }

    public Task UpdateAsync(Customer customer)
    {
        _customers[customer.Id] = customer;
        return Task.CompletedTask;
    }
}
