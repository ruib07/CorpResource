using CorpResource.Domain.Enums;

namespace CorpResource.Domain.Models;

public class ResourceAssignment
{
    public Guid Id { get; set; }
    public Guid ResourceId { get; set; }
    public Resource Resource { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime AssignedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public ResourceAssignmentsStatus Status { get; set; }
    public string Notes { get; set; }
}
