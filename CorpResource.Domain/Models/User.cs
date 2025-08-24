using CorpResource.Domain.Enums;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public Department? Department { get; set; }

    public Department? ManagedDepartment { get; set; }
    public ICollection<ResourceAssignment> ResourceAssignments { get; set; }
    public ICollection<Notification> Notifications { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; }
}
