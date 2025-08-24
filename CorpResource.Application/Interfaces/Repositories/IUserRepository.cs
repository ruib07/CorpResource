using CorpResource.Domain.Enums;
using CorpResource.Domain.Models;

namespace CorpResource.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetByRoleAsync(Roles role);
    Task SaveAsync(User user);
    Task DeleteAsync(Guid id);
}
