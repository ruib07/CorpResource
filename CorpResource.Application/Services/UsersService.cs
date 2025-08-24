using CorpResource.Application.Interfaces.Repositories;
using CorpResource.Application.Interfaces.Services;
using CorpResource.Application.Shared.Common;
using CorpResource.Domain.Enums;
using CorpResource.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CorpResource.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUserRepository _userRepository;

    public UsersService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? 
            throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<Result<User>> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            return Result<User>.Fail("User not found.", 404);

        return Result<User>.Success(user);
    }

    public async Task<IEnumerable<User>> GetByRoleAync(Roles role)
    {
        return await _userRepository.GetByRoleAsync(role);
    }

    public async Task<Result<User>> SaveAsync(User user)
    {
        if (user is null)
            return Result<User>.Fail("User cannot be null.", 400);

        try
        {
            await _userRepository.SaveAsync(user);

            return Result<User>.Success(user, "User saved successfully.");
        }
        catch (ValidationException ex)
        {
            return Result<User>.Fail($"Validation failed: {ex.Message}", 400);
        }
        catch (Exception ex)
        {
            return Result<User>.Fail($"An error occurred: {ex.Message}", 500);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
    }
}
