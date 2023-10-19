using Base.Domain;

namespace App.DAL.DTO;

public class Activity : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public ICollection<Event>? Events { get; set; }
}