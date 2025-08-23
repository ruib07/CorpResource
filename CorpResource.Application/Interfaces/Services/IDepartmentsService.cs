using CorpResource.Application.Shared.Common;
using CorpResource.Domain.Models;

namespace CorpResource.Application.Interfaces.Services;

public interface IDepartmentsService
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task<Result<Department>> GetByIdAsync(Guid id);
    Task<Result<Department>> SaveAsync(Department department);
    Task DeleteAsync(Guid id);
}
