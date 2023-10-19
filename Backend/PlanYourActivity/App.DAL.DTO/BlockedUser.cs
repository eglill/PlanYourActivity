using App.DAL.DTO.identity;
using Base.Domain;

namespace App.DAL.DTO;

public class BlockedUser : DomainEntityId
{
    public DateTime CreatedAt { get; set; } = default!;
    
    public Guid BlockerId { get; set; }
    public AppUser? Blocker { get; set; }
    
    public Guid BlockedAppUserId { get; set; }
    public AppUser? BlockedAppUser { get; set; }
}