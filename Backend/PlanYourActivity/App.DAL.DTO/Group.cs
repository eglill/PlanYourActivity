using App.Domain.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Group : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int MaxParticipants { get; set; }
    public int? MaxAge { get; set; }
    public int? MinAge { get; set; }
    public bool Private { get; set; }
    public bool JoiningLocked { get; set; }
    
    public Guid AdminId { get; set; }
    public AppUser? Admin { get; set; }
    
    public Guid? GroupGenderId { get; set; }
    public Gender? GroupGender { get; set; }
    
    public ICollection<EventInGroup>? EventsInGroup { get; set; }
    public ICollection<UserInGroup>? UsersInGroup { get; set; }
    public ICollection<InviteToGroup>? InvitesToGroup { get; set; }
}