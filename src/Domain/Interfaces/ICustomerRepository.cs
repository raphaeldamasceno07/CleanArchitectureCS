using Domain.Entities;

namespace Domain.Interfaces;

public interface ICustomerRepository
{
    Task AddAsync(Customer ccstomer);
    Task<Customer?> GetByCpfAsync(string cpf);
    Task<Customer?> GetByIdAsync(Guid id);
    Task UpdateAsync(Customer ccstomer);
}
