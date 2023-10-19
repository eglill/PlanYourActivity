using App.BLL.DTO;
using App.BLL.DTO.Identity;
using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class ConversationMapper : BaseMapper<Conversation, Public.DTO.v1.Conversation>
{
    public ConversationMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public AddMessage MapAddMessage(Public.DTO.v1.AddMessage entity)
    {
        return Mapper.Map<AddMessage>(entity);
    }

}