using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Location : DomainEntityId
{
    [MaxLength(512)]
    public string Address { get; set; } = default!;

    [MaxLength(512)]
    public string? Description { get; set; }
    
    public ICollection<Event>? Events { get; set; }
    
    public Guid CountryId { get; set; }
    public Country? Country { get; set; }
}