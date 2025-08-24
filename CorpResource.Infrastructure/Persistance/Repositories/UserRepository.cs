using CorpResource.Application.Interfaces.Repositories;
using CorpResource.Domain.Enums;
using CorpResource.Domain.Models;
using CorpResource.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CorpResource.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CorpResourceDbContext _context;
    private DbSet<User> Users => _context.Users;
    private DbSet<Department> Departments => _context.Departments;

    public UserRepository(CorpResourceDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await Users.AsNoTracking().ToListAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<User>> GetByRoleAsync(Roles role)
    {
        return await Users.AsNoTracking()
                          .Where(u => u.Role == role)
                          .ToListAsync();
    }

    public async Task SaveAsync(User user)
    {
        if (user.Id == Guid.Empty)
        {
            await Users.AddAsync(user);
            await _context.SaveChangesAsync();

            if (user.DepartmentId != null)
            {
                var department = await Departments.FirstOrDefaultAsync(d => d.Id == user.DepartmentId);

                if (department is not null && department.ManagerId == null)
                {
                    department.ManagerId = user.Id;
                    Departments.Update(department);
                    await _context.SaveChangesAsync();
                }
            }
        }
        else
        {
            var validationContext = new ValidationContext(user);
            Validator.ValidateObject(user, validationContext);

            Users.Update(user);
            _context.SaveChanges();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await GetByIdAsync(id);

        Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
