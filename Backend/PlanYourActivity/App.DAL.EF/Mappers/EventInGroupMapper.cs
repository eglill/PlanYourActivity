using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class EventInGroupMapper : BaseMapper<Domain.EventInGroup, EventInGroup>
{
    public EventInGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}