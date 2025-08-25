using CorpResource.Domain.Models;

namespace CorpResource.APIClient.Contracts;

public interface IResourcesApiService
{
    Task<IEnumerable<Resource>> GetResources();
    Task<Resource> GetResource(Guid id);
    Task<Resource> SaveResource(Resource resource);
    Task DeleteResource(Guid id);
}
