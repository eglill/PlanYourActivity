using Base.Domain;

namespace App.DAL.DTO;

public class PollOption : DomainEntityId
{
    public string Option { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<UserOptionChoice>? UsersOptionChoice { get; set; }
    
    public Guid PollQuestionId { get; set; }
    public PollQuestion? PollQuestion { get; set; }
}