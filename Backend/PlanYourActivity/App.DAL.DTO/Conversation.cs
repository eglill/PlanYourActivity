using Base.Domain;

namespace App.DAL.DTO;

public class Conversation : DomainEntityId
{
    public string? Name { get; set; }
    
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = null!;
    
    public ICollection<Message>? Messages { get; set; }
}