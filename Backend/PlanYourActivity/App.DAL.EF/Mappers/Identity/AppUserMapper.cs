using App.DAL.DTO.identity;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers.Identity;

public class AppUserMapper : BaseMapper<Domain.Identity.AppUser, AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}