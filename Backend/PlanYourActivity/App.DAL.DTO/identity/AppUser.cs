using Base.Domain;

namespace App.DAL.DTO.identity;

public class AppUser : DomainEntityId
{
    public string FirstName { get; set; } = default!;

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
    public ICollection<BlockedUser>? BlockedUsers { get; set; }
    public ICollection<BlockedUser>? UsersWhoBlockedThisUser { get; set; }
    public ICollection<FavoriteUser>? FavoriteUsers { get; set; }
}
