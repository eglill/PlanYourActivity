using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ConversationMapper : BaseMapper<DAL.DTO.Conversation, Conversation>
{
    public ConversationMapper(IMapper mapper) : base(mapper)
    {
    }
}