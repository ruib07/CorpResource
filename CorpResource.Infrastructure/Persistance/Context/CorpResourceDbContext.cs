using CorpResource.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CorpResource.Infrastructure.Persistance.Context;

public class CorpResourceDbContext : DbContext
{
    public CorpResourceDbContext(DbContextOptions<CorpResourceDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<ResourceAssignment> ResourceAssignments { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
