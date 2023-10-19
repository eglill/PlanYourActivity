using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Colour : DomainEntityId
{
    [MaxLength(128)]
    public string Hex { get; set; } = default!;
    
    public ICollection<Event>? Events { get; set; }
}