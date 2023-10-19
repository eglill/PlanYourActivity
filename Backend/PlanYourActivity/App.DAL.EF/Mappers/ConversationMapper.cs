using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ConversationMapper : BaseMapper<Domain.Conversation, Conversation>
{
    public ConversationMapper(IMapper mapper) : base(mapper)
    {
    }
}