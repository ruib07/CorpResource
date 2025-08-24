using CorpResource.Application.Interfaces.Services;
using CorpResource.Domain.Enums;
using CorpResource.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorpResource.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService ??
            throw new ArgumentNullException(nameof(usersService));
    }

    // GET api/v1/users
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return Ok(await _usersService.GetAllAsync());
    }

    // GET api/v1/users/by-id/{id}
    [HttpGet("by-id/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var result = await _usersService.GetByIdAsync(id);

        if (!result.IsSuccess) return StatusCode(result.Error.StatusCode, result.Error);

        return Ok(result.Data);
    }

    // GET api/v1/users/by-role/{role}
    [HttpGet("by-role/{role}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUsersByRole(Roles role)
    {
        return Ok(await _usersService.GetByRoleAync(role));
    }

    // POST api/v1/users
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SaveUser([FromBody] User user)
    {
        var result = await _usersService.SaveAsync(user);

        if (!result.IsSuccess)
            return StatusCode(result.Error.StatusCode, result.Error);

        var statusCode = user.Id == Guid.Empty ? StatusCodes.Status201Created : StatusCodes.Status200OK;

        return StatusCode(statusCode, result.Data);
    }

    // DELETE api/v1/users/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _usersService.DeleteAsync(id);

        return NoContent();
    }
}
