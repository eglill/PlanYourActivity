namespace Public.DTO.v1;

public class Location
{
    public Guid Id { get; set; }
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