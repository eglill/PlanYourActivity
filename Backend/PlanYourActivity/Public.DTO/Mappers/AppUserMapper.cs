using App.BLL.DTO.Identity;
using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class AppUserMapper : BaseMapper<AppUser, Public.DTO.v1.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}