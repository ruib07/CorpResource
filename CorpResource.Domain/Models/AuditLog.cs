using CorpResource.Domain.Enums;

namespace CorpResource.Domain.Models;

public class AuditLog
{
    public Guid Id { get; set; }
    public AuditLogsEntities Entity { get; set; }
    public Guid EntityId { get; set; }
    public AuditLogsActions Action { get; set; }
    public Guid PerformedBy { get; set; }
    public User PerformedByUser { get; set; }
    public DateTime PerformedAt { get; set; }
    public string Details { get; set; }
}
