using CorpResource.Application.Shared.Common;
using CorpResource.Domain.Enums;
using CorpResource.Domain.Models;

namespace CorpResource.Application.Interfaces.Services;

public interface IUsersService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<Result<User>> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetByRoleAync(Roles role);
    Task<Result<User>> SaveAsync(User user);
    Task DeleteAsync(Guid id);
}
