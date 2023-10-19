using Base.Domain;

namespace App.BLL.DTO;

public class Activity : DomainEntityId
{
    public string Name { get; set; } = default!;
}