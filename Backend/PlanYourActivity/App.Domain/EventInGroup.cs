using Base.Domain;

namespace App.Domain;

public class EventInGroup : DomainEntityId
{
    public Guid GroupId { get; set; }
    public Group? Group { get; set; }
    
    public Guid EventId { get; set; }
    public Event? Event { get; set; }
}