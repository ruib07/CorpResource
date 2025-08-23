namespace CorpResource.Domain.Enums;

public enum Roles
{
    Admin,
    User
}

public enum ResourceStatus
{
    Available,
    InUse,
    Inactive
}

public enum ResourceType
{
    Laptop,
    Monitor,
    Phone,
    Tablet,
    Room,
    Other
}

public enum ResourceAssignmentsStatus
{
    Active,
    Finished,
    Canceled
}

public enum AuditLogsEntities
{
    Users,
    Resources,
    ResourceAssignments,
    Departments,
    Notifications
}

public enum AuditLogsActions
{
    Create,
    Update,
    Delete
}