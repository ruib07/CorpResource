using CorpResource.Application.Interfaces.Services;
using CorpResource.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorpResource.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ResourcesController : ControllerBase
{
    private readonly IResourcesService _resourcesService;

    public ResourcesController(IResourcesService resourcesService)
    {
        _resourcesService = resourcesService ??
            throw new ArgumentNullException(nameof(resourcesService));
    }

    // GET api/v1/resources
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Resource>>> GetResources()
    {
        return Ok(await _resourcesService.GetAllAsync());
    }

    // GET api/v1/resources/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetResourceById(Guid id)
    {
        var result = await _resourcesService.GetByIdAsync(id);

        if (!result.IsSuccess) return StatusCode(result.Error.StatusCode, result.Error);

        return Ok(result.Data);
    }

    // POST api/v1/resources
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SaveResource([FromBody] Resource resource)
    {
        var result = await _resourcesService.SaveAsync(resource);

        if (!result.IsSuccess)
            return StatusCode(result.Error.StatusCode, result.Error);

        var statusCode = resource.Id == Guid.Empty ? StatusCodes.Status201Created : StatusCodes.Status200OK;

        return StatusCode(statusCode, result.Data);
    }

    // DELETE api/v1/resources/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteResource(Guid id)
    {
        await _resourcesService.DeleteAsync(id);

        return NoContent();
    }
}
