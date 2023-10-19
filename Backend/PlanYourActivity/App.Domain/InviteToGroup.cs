using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class InviteToGroup : DomainEntityId
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid GroupId { get; set; }
    public Group? Group { get; set; }
}