using App.DAL.DTO.identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Event : DomainEntityId
{
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

public class EventFilter
{
    public DateTime? TimeFrom { get; set; }
    public DateTime? TimeTo { get; set; }
    public Guid? CountryId { get; set; }
    public Guid? ActivityId { get; set; } 
}