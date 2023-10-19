using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class PollQuestion : DomainEntityId
{
    [MaxLength(244)]
    public string Question { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime Deadline { get; set; } = default!;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<PollOption>? PollOptions { get; set; }
    
    public Guid CreatorId { get; set; }
    public UserInGroup? Creator { get; set; }
}