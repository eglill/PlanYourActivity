namespace Public.DTO.v1;

public class Conversation
{
    public ICollection<Message> Messages { get; set; } = default!;
}