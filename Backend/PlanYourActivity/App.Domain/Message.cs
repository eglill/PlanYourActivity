using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Message : DomainEntityId
{
    [MaxLength(1024)]
    public string Content { get; set; } = default!;
    
    public DateTime CreatedAt { get; set; } = default!;
    
    public Guid CreatorId { get; set; }
    public AppUser? Creator { get; set; }
    
    public Guid ConversationId { get; set; }
    public Conversation? Conversation { get; set; }
}