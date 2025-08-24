using CorpResource.Domain.Enums;
using CorpResource.Domain.Models;

namespace CorpResource.APIClient.Contracts;

public interface IUsersApiService
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUser(Guid id);
    Task<IEnumerable<User>> GetUsersByRole(Roles role);
    Task<User> SaveUser(User user);
    Task DeleteUser(Guid id);
}
