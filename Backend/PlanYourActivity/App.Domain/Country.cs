using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Country : DomainEntityId
{
    [MaxLength(512)]
    public string Name { get; set; } = default!;
    
    public ICollection<Location>? Locations { get; set; }
}