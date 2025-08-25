using CorpResource.Application.Interfaces.Repositories;
using CorpResource.Application.Interfaces.Services;
using CorpResource.Application.Shared.Common;
using CorpResource.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CorpResource.Application.Services;

public class ResourcesService : IResourcesService
{
    private readonly IResourceRepository _resourceRepository;

    public ResourcesService(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository ??
            throw new ArgumentNullException(nameof(resourceRepository));
    }

    public async Task<IEnumerable<Resource>> GetAllAsync()
    {
        return await _resourceRepository.GetAllAsync();
    }

    public async Task<Result<Resource>> GetByIdAsync(Guid id)
    {
        var resource = await _resourceRepository.GetByIdAsync(id);

        if (resource is null)
            return Result<Resource>.Fail("Resource not found.", 404);

        return Result<Resource>.Success(resource);
    }

    public async Task<Result<Resource>> SaveAsync(Resource resource)
    {
        if (resource is null)
            return Result<Resource>.Fail("Resource cannot be null.", 400);

        try
        {
            await _resourceRepository.SaveAsync(resource);

            return Result<Resource>.Success(resource, "Resource saved successfully.");
        }
        catch (ValidationException ex)
        {
            return Result<Resource>.Fail($"Validation failed: {ex.Message}", 400);
        }
        catch (Exception ex)
        {
            return Result<Resource>.Fail($"An error occurred: {ex.Message}", 500);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        await _resourceRepository.DeleteAsync(id);
    }
}
