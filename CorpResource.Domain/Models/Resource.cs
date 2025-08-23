using CorpResource.Domain.Enums;

namespace CorpResource.Domain.Models;

public class Resource
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ResourceType Type { get; set; }
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }
    public ResourceStatus Status { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<ResourceAssignment> ResourceAssignments { get; set; }
}
