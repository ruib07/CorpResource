using CorpResource.Domain.Enums;

namespace CorpResource.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public Roles Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? DepartmentId { get; set; }
    public ICollection<Department> Departments { get; set; }
    public ICollection<ResourceAssignment> ResourceAssignments { get; set; }
    public ICollection<Notification> Notifications { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; }
}
