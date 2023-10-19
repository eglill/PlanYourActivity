using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserEventMapper : BaseMapper<Domain.UserEvent, UserEvent>
{
    public UserEventMapper(IMapper mapper) : base(mapper)
    {
    }
}