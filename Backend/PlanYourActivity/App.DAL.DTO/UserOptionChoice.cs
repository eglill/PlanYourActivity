using Base.Domain;

namespace App.DAL.DTO;

public class UserOptionChoice : DomainEntityId
{
    public DateTime CratedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid PollOptionId { get; set; }
    public PollOption? PollOption { get; set; }
    
    public Guid UserInGroupId { get; set; }
    public UserInGroup? UserInGroup { get; set; }
}