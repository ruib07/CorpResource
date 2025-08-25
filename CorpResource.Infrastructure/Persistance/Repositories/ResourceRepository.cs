using CorpResource.Application.Interfaces.Repositories;
using CorpResource.Domain.Models;
using CorpResource.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CorpResource.Infrastructure.Persistance.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly CorpResourceDbContext _context;
    private DbSet<Resource> Resources => _context.Resources;

    public ResourceRepository(CorpResourceDbContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Resource>> GetAllAsync()
    {
        return await Resources.AsNoTracking().ToListAsync();
    }

    public async Task<Resource> GetByIdAsync(Guid id)
    {
        return await Resources.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task SaveAsync(Resource resource)
    {
        if (resource.Id == Guid.Empty)
        {
            await Resources.AddAsync(resource);
            await _context.SaveChangesAsync();
        }
        else
        {
            var validationContext = new ValidationContext(resource);
            Validator.ValidateObject(resource, validationContext);

            Resources.Update(resource);
            _context.SaveChanges();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var resource = await GetByIdAsync(id);

        Resources.Remove(resource);
        await _context.SaveChangesAsync();
    }
}
