using CorpResource.Domain.Models;

namespace CorpResource.Application.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task<Department> GetByIdAsync(Guid id);
    Task SaveAsync(Department department);
    Task DeleteAsync(Guid id);
}
