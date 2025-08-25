using CorpResource.Application.Shared.Common;
using CorpResource.Domain.Models;

namespace CorpResource.Application.Interfaces.Services;

public interface IResourcesService
{
    Task<IEnumerable<Resource>> GetAllAsync();
    Task<Result<Resource>> GetByIdAsync(Guid id);
    Task<Result<Resource>> SaveAsync(Resource resource);
    Task DeleteAsync(Guid id);
}
