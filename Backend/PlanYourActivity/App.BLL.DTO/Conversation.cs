using Base.Domain;

namespace App.BLL.DTO;

public class Conversation  : DomainEntityId
{
    public ICollection<Message> Messages { get; set; } = default!;
}