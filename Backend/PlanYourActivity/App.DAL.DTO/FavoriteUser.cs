using App.Domain.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class FavoriteUser : DomainEntityId
{
    public DateTime CreatedAt { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}