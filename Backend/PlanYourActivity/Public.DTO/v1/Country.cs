namespace Public.DTO.v1;

/// <summary>
/// Used for Location
/// </summary>
public class Country
{
    /// <summary>
    /// Country Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Country name (e.g. Estonia)
    /// </summary>
    /// /// <example>Estonia</example>
    public string Name { get; set; } = default!;
}