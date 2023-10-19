namespace App.BLL.DTO;

public class Message
{
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = default!;
    public string Creator { get; set; } = default!;
}

public class AddMessage
{
    public string Content { get; set; } = default!;
    public Guid groupId { get; set; } = default!;
}
