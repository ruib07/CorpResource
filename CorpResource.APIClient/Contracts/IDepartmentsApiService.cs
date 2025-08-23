using CorpResource.Domain.Models;

namespace CorpResource.APIClient.Contracts;

public interface IDepartmentsApiService
{
    Task<IEnumerable<Department>> GetDepartments();
    Task<Department> GetDepartment(Guid id);
    Task<Department> SaveDepartment(Department department);
    Task DeleteDepartment(Guid id);
}
