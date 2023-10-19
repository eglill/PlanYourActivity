namespace Public.DTO.v1;

/// <summary>
/// Activity class
/// </summary>
public class Activity
{
    /// <summary>
    /// Activity Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Activity name (e.g. Basketball)
    /// </summary>
    /// /// <example>Basketball</example>
    public string Name { get; set; } = default!;
}