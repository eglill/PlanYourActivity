using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class MessageMapper : BaseMapper<Domain.Message, Message>
{
    public MessageMapper(IMapper mapper) : base(mapper)
    {
    }
}