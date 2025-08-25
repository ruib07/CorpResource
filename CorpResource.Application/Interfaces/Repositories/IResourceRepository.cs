using CorpResource.Domain.Models;

namespace CorpResource.Application.Interfaces.Repositories;

public interface IResourceRepository
{
    Task<IEnumerable<Resource>> GetAllAsync();
    Task<Resource> GetByIdAsync(Guid id);
    Task SaveAsync(Resource resource);
    Task DeleteAsync(Guid id);
}
