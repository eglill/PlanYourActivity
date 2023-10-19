using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Activity : DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public ICollection<Event>? Events { get; set; }
}