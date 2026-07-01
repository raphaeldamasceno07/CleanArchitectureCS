using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserByIdAsync(Guid id);
    Task UpdateAsync(User user);
    // Patch: mark file as read (no functional change)
}
