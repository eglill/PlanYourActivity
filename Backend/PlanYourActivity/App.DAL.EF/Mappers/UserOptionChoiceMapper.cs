using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserOptionChoiceMapper : BaseMapper<Domain.UserOptionChoice, UserOptionChoice>
{
    public UserOptionChoiceMapper(IMapper mapper) : base(mapper)
    {
    }
}