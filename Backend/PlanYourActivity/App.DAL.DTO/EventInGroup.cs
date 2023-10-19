using Base.Domain;

namespace App.DAL.DTO;

public class EventInGroup : DomainEntityId
{
    public Guid GroupId { get; set; }
    public Group? Group { get; set; }
    
    public Guid EventId { get; set; }
    public Event? Event { get; set; }
}