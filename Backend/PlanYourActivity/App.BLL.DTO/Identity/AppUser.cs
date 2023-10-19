using Base.Domain;

namespace App.BLL.DTO.Identity;

public class AppUser : DomainEntityId
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly BirthDate { get; set; } = default!;
}