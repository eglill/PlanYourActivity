using Base.Domain;

namespace App.DAL.DTO;

public class Country : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public ICollection<Location>? Locations { get; set; }
}