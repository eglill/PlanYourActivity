using App.DAL.DTO;
using App.Domain.Identity;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserInGroupMapper : BaseMapper<Domain.UserInGroup, UserInGroup>
{
    public UserInGroupMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public DTO.identity.AppUser MapAppUser(AppUser eventFilter)
    {
        return Mapper.Map<DTO.identity.AppUser>(eventFilter);
    }
}