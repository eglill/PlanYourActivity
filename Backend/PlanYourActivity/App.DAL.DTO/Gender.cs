using App.Domain.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Gender : DomainEntityId
{
    public string Name { get; set; } = default!;

    public ICollection<Group>? Groups { get; set; }

    public ICollection<AppUser>? AppUsers { get; set; }
}