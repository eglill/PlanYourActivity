using Base.Domain;

namespace App.DAL.DTO;

public class PollQuestion : DomainEntityId
{
    public string Question { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime Deadline { get; set; } = default!;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<PollOption>? PollOptions { get; set; }
    
    public Guid CreatorId { get; set; }
    public UserInGroup? Creator { get; set; }
}