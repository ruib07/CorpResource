using CorpResource.Application.Interfaces.Services;
using CorpResource.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorpResource.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentsService _departmentsService;

    public DepartmentsController(IDepartmentsService departmentsService)
    {
        _departmentsService = departmentsService ?? 
            throw new ArgumentNullException(nameof(departmentsService));
    }

    // GET api/v1/departments
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        return Ok(await _departmentsService.GetAllAsync());
    }

    // GET api/v1/departments/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDepartmentById(Guid id)
    {
        var result = await _departmentsService.GetByIdAsync(id);

        if (!result.IsSuccess) return StatusCode(result.Error.StatusCode, result.Error);

        return Ok(result.Data);
    }

    // POST api/v1/departments
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SaveDepartment([FromBody] Department department)
    {
        var result = await _departmentsService.SaveAsync(department);

        if (!result.IsSuccess)
            return StatusCode(result.Error.StatusCode, result.Error);

        var statusCode = department.Id == Guid.Empty ? StatusCodes.Status201Created : StatusCodes.Status200OK;

        return StatusCode(statusCode, result.Data);
    }

    // DELETE api/v1/departments/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDepartment(Guid id)
    {
        await _departmentsService.DeleteAsync(id);

        return NoContent();
    }
}
