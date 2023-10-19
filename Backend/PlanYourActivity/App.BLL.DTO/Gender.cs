using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using App.Domain.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Gender : DomainEntityId
{
    public string Name { get; set; } = default!;
}