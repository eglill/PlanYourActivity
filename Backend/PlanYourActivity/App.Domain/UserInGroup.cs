using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserInGroup : DomainEntityId
{
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LeftAt { get; set; }
    
    public Guid GroupId { get; set; }
    public Group? Group { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public ICollection<UserOptionChoice>? UserOptionChoices { get; set; }
    public ICollection<PollQuestion>? PollQuestions { get; set; }
}