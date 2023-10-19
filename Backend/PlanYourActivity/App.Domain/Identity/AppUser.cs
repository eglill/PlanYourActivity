using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;

    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    public DateOnly BirthDate { get; set; } = default!;
    
    public Guid GenderId { get; set; }
    public Gender Gender { get; set; } = default!;

    public ICollection<Group>? Groups { get; set; }
    public ICollection<UserEvent>? UserEvents { get; set; }
    public ICollection<InviteToGroup>? InviteToGroups { get; set; }
    public ICollection<Message>? Messages { get; set; }
    public ICollection<Event>? CreatedEvents { get; set; }
    public ICollection<UserInGroup>? UserInGroups { get; set; }
    
    // [InverseProperty("BlockedAppUser")]
    [InverseProperty("Blocker")]
    public ICollection<BlockedUser>? BlockedUsers { get; set; }
    // [InverseProperty("Blocker")]
    [InverseProperty("BlockedAppUser")]
    public ICollection<BlockedUser>? UsersWhoBlockedThisUser { get; set; }
    
    
    public ICollection<FavoriteUser>? FavoriteUsers { get; set; }
    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}
