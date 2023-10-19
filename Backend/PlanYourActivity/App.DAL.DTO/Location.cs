using Base.Domain;

namespace App.DAL.DTO;

public class Location : DomainEntityId
{
    public string Address { get; set; } = default!;

    public string? Description { get; set; }
    
    public ICollection<Domain.Event>? Events { get; set; }
    
    public Guid CountryId { get; set; }
    public Country? Country { get; set; }
}