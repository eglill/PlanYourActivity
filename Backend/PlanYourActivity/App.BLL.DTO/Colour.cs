using Base.Domain;

namespace App.BLL.DTO;

public class Colour : DomainEntityId
{
    public string Hex { get; set; } = default!;
}