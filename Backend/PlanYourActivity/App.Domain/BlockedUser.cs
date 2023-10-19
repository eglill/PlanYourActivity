using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class BlockedUser : DomainEntityId
{
    public DateTime CreatedAt { get; set; } = default!;
    
    [ForeignKey("Blocker")]
    public Guid BlockerId { get; set; }
    public AppUser? Blocker { get; set; }
    
    [ForeignKey("BlockedAppUser")]
    public Guid BlockedAppUserId { get; set; }
    public AppUser? BlockedAppUser { get; set; }
}