using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class InviteToGroupMapper : BaseMapper<Domain.InviteToGroup, InviteToGroup>
{
    public InviteToGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}