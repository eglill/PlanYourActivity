using Base.Domain;

namespace App.BLL.DTO;

public class Location: DomainEntityId
{
    public string Address { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public string Country { get; set; } = default!;
}

public class LocationAdd
{
    public string Address { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public Guid CountryId { get; set; } = default!;
}