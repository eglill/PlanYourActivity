using App.BLL.DTO;
using App.DAL.DTO.identity;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class GroupMapper : BaseMapper<DAL.DTO.Group, Group>
{
    public GroupMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public GroupWithData MapToGroupWithData(DAL.DTO.Group entity)
    {
        return Mapper.Map<GroupWithData>(entity);
    }

    public DAL.DTO.Group MapToGroup(AddGroup entity)
    {
        return Mapper.Map<DAL.DTO.Group>(entity);
    }
    
    public App.BLL.DTO.Identity.AppUser MapToAppUser(AppUser entity)
    {
        return Mapper.Map<App.BLL.DTO.Identity.AppUser>(entity);
    }
        
}