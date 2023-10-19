namespace Public.DTO.v1;

/// <summary>
/// Colour class
/// </summary>
public class Colour
{
    /// <summary>
    /// Colour Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Colour hex code
    /// </summary>
    /// /// <example>#00ff33</example>
    public string Hex { get; set; } = default!;
}