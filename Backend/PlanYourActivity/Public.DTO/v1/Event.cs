using App.Domain;

namespace Public.DTO.v1;

public class EventFilter
{
    public DateTime? TimeFrom { get; set; }
    public DateTime? TimeTo { get; set; }
    public Guid? CountryId { get; set; }
    public Guid? ActivityId { get; set; } 
}

public class GroupEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string Colour { get; set; } = default!;
    public Location Location { get; set; } = default!;
    public string Activity { get; set; } = default!;
}

public class UserEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string Colour { get; set; } = default!;
    public Location? Location { get; set; }
    public string? Activity { get; set; }
    public bool AvailableForGroupEvent { get; set; }
    public bool Editable { get; set; }
}

public class AddGroupEvent
{
    public string Name { get; set; } = default!;
    public DateTime StartsAt { get; set; } = default!;
    public DateTime EndsAt { get; set; } = default!;
    public Guid ColourId { get; set; } = default!;
    public LocationAdd Location { get; set; } = default!;
    public Guid ActivityId { get; set; } = default!;
    public Guid GroupId { get; set; } = default!;
}

public class AddUserEvent
{
    public string Name { get; set; } = default!;
    public DateTime StartsAt { get; set; } = default!;
    public DateTime EndsAt { get; set; } = default!;
    public Guid ColourId { get; set; } = default!;
    public LocationAdd? Location { get; set; } = default!;
    public Guid? ActivityId { get; set; } = default!;
    public bool AvailableForGroupEvent { get; set; } = default!;
}