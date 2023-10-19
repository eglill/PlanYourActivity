using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Group : DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    [MaxLength(1024)]
    public string Description { get; set; } = default!;
    public int MaxParticipants { get; set; }
    public int? MaxAge { get; set; }
    public int? MinAge { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public bool Private { get; set; }
    public bool JoiningLocked { get; set; }

    
    public Guid AdminId { get; set; }
    public AppUser? Admin { get; set; }
    
    public Guid? GroupGenderId { get; set; }
    public Gender? GroupGender { get; set; }
    
    public Conversation? Conversation { get; set; }
    
    public ICollection<EventInGroup>? EventsInGroup { get; set; }
    public ICollection<UserInGroup>? UsersInGroup { get; set; }
    public ICollection<InviteToGroup>? InvitesToGroup { get; set; }
}