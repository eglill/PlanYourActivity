using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class BlockedUserMapper : BaseMapper<Domain.BlockedUser, BlockedUser>
{
    public BlockedUserMapper(IMapper mapper) : base(mapper)
    {
    }
}