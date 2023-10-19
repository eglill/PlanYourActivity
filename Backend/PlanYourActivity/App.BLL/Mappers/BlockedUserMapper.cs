using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class BlockedUserMapper : BaseMapper<DAL.DTO.BlockedUser, BlockedUser>
{
    public BlockedUserMapper(IMapper mapper) : base(mapper)
    {
    }
}