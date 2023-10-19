using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class PollOption : DomainEntityId
{
    [MaxLength(128)]
    public string Option { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<UserOptionChoice>? UsersOptionChoice { get; set; }
    
    public Guid PollQuestionId { get; set; }
    public PollQuestion? PollQuestion { get; set; }
}