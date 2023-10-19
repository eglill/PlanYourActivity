using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Event : DomainEntityId
{
    [MaxLength(1024)]
    public string Name { get; set; } = default!;
    
    public DateTime StartsAt { get; set; } = default!;
    
    public DateTime EndsAt { get; set; } = default!;
    
    public ICollection<UserEvent>? UsersEvent { get; set; }
    public ICollection<EventInGroup>? EventInGroups { get; set; }
    
    public Guid? CreatorId { get; set; }
    public AppUser? Creator { get; set; }
    
    public Guid? ActivityId { get; set; }
    public Activity? Activity { get; set; }
    
    public Guid? LocationId { get; set; }
    public Location? Location { get; set; }
    
    public Guid ColourId { get; set; }
    public Colour? Colour { get; set; }
}