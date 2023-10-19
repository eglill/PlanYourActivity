using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Gender : DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public ICollection<Group>? Groups { get; set; }
    
    public ICollection<AppUser>? AppUsers { get; set; }
}