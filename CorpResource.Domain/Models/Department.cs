namespace CorpResource.Domain.Models;

public class Department
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ManagerId { get; set; }
    public User Manager { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<User> Users { get; set; }
    public ICollection<Resource> Resources { get; set; }
}
