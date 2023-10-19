namespace Public.DTO.v1.Identity;

public class AppUser
{
    public Guid Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly BirthDate { get; set; } = default!;
}