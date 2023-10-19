using Base.Domain;

namespace App.DAL.DTO;

public class Colour : DomainEntityId
{
    public string Hex { get; set; } = default!;
    
    public ICollection<Event>? Events { get; set; }
}