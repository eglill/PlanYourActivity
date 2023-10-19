using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class EventMapper : BaseMapper<GroupEvent, Public.DTO.v1.GroupEvent>
{
    public EventMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public EventFilter? MapEventFilter(Public.DTO.v1.EventFilter? eventFilter)
    {
        return Mapper.Map<EventFilter>(eventFilter);
    }
    
    public Public.DTO.v1.UserEvent MapUserEvent(UserEvent entity)
    {
        return Mapper.Map<Public.DTO.v1.UserEvent>(entity);
    }

    public AddGroupEvent MapEventWithGroup(Public.DTO.v1.AddGroupEvent entity)
    {
        return Mapper.Map<AddGroupEvent>(entity);
    }
    
    public AddUserEvent MapEventWithUser(Public.DTO.v1.AddUserEvent entity)
    {
        return Mapper.Map<AddUserEvent>(entity);
    }
}