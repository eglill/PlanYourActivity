using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class EventMapper : BaseMapper<DAL.DTO.Event, GroupEvent>
{
    public EventMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public DAL.DTO.EventFilter? MapEventFilter(EventFilter? eventFilter)
    {
        return Mapper.Map<DAL.DTO.EventFilter>(eventFilter);
    }
    
    public UserEvent MapToUserEvent(DAL.DTO.Event entity)
    {
        return Mapper.Map<UserEvent>(entity);
    }
    
    public DAL.DTO.Event MapToEvent(AddGroupEvent entity)
    {
        return Mapper.Map<DAL.DTO.Event>(entity);
    }
    
    public DAL.DTO.Event MapToEvent(AddUserEvent entity)
    {
        return Mapper.Map<DAL.DTO.Event>(entity);
    }
    
}