using App.BLL.DTO.Identity;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class AppUserMapper : BaseMapper<DAL.DTO.identity.AppUser, AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}