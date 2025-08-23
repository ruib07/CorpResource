using CorpResource.Application.Interfaces.Repositories;
using CorpResource.Domain.Models;
using CorpResource.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CorpResource.Infrastructure.Persistance.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly CorpResourceDbContext _context;
    private DbSet<Department> Departments => _context.Departments;

    public DepartmentRepository(CorpResourceDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await Departments.AsNoTracking().ToListAsync();
    }

    public async Task<Department> GetByIdAsync(Guid id)
    {
        return await Departments.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task SaveAsync(Department department)
    {
        if (department.Id == Guid.Empty)
        {
            await Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }
        else 
        {
            var validationContext = new ValidationContext(department);
            Validator.ValidateObject(department, validationContext);

            Departments.Update(department);
            _context.SaveChanges();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var department = await GetByIdAsync(id);

        Departments.Remove(department);
        await _context.SaveChangesAsync();
    }
}
