using Base.Domain;

namespace Public.DTO.v1;

/// <summary>
/// Used for User and Group
/// </summary>
public class Gender
{
    /// <summary>
    /// Gender Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gender name (e.g. Male)
    /// </summary>
    /// /// <example>Male</example>
    public string Name { get; set; } = default!;
}