using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Conversation : DomainEntityId
{
    [MaxLength(128)]
    public string? Name { get; set; }
    
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public ICollection<Message>? Messages { get; set; }
}