using CorpResource.Application.Interfaces.Repositories;
using CorpResource.Application.Interfaces.Services;
using CorpResource.Application.Shared.Common;
using CorpResource.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CorpResource.Application.Services;

public class DepartmentsService : IDepartmentsService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentsService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository ?? 
            throw new ArgumentNullException(nameof(departmentRepository));
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _departmentRepository.GetAllAsync();
    }

    public async Task<Result<Department>> GetByIdAsync(Guid id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);

        if (department is null)
            return Result<Department>.Fail("Department not found.", 404);

        return Result<Department>.Success(department);
    }

    public async Task<Result<Department>> SaveAsync(Department department)
    {
        if (department is null)
            return Result<Department>.Fail("Department cannot be null.", 400);

        try
        {
            await _departmentRepository.SaveAsync(department);

            return Result<Department>.Success(department, "Department saved successfully.");
        }
        catch (ValidationException ex)
        {
            return Result<Department>.Fail($"Validation failed: {ex.Message}", 400);
        }
        catch (Exception ex)
        {
            return Result<Department>.Fail($"An error occurred: {ex.Message}", 500);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        await _departmentRepository.DeleteAsync(id);
    }
}
