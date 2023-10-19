using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserEvent : DomainEntityId
{
    public bool AvailableForGroupEvent { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid EventId { get; set; }
    public Event? Event { get; set; }
}