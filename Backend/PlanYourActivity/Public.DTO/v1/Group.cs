using App.Domain.Identity;

namespace Public.DTO.v1;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int MaxParticipants { get; set; }
    public int Participants { get; set; }
    public int? MaxAge { get; set; }
    public int? MinAge { get; set; }
    public string? GroupGender { get; set; } = default!;
}

public class GroupWithData
{
    // TODO free time object
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int MaxParticipants { get; set; }
    public int Participants { get; set; }
    public int? MaxAge { get; set; }
    public int? MinAge { get; set; }
    public bool Private { get; set; }
    public bool JoiningLocked { get; set; }
    public string? GroupGender { get; set; } = default!;
    public string Admin { get; set; } = default!;
}

public class AddGroup
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int MaxParticipants { get; set; }
    public int? MaxAge { get; set; }
    public int? MinAge { get; set; }
    public bool Private { get; set; }
    public bool JoiningLocked { get; set; }
    public Guid? GroupGenderId { get; set; }
}

public class GroupAdminAction
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
}