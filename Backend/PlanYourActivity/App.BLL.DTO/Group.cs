using App.Domain.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Group : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int MaxParticipants { get; set; }
    public int Participants { get; set; }
    public int? MaxAge { get; set; }
    public int? MinAge { get; set; }
    public string? GroupGender { get; set; } = default!;
}

public class GroupWithData : DomainEntityId
{
    // TODO free time object
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

public class AddGroup : DomainEntityId
{
    public Guid AdminId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int MaxParticipants { get; set; }
    public int? MaxAge { get; set; }
    public int? MinAge { get; set; }
    public bool Private { get; set; }
    public bool JoiningLocked { get; set; }
    public Guid? GroupGenderId { get; set; }
}