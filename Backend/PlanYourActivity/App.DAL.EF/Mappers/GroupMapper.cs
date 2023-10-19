using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class GroupMapper : BaseMapper<Domain.Group, Group>
{
    public GroupMapper(IMapper mapper) : base(mapper)
    {
    }
}