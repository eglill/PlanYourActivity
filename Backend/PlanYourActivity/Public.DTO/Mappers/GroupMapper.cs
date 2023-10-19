using App.BLL.DTO;
using App.BLL.DTO.Identity;
using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class GroupMapper : BaseMapper<Group, Public.DTO.v1.Group>
{
    public GroupMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public Public.DTO.v1.GroupWithData MapToGroupWithData(GroupWithData entity)
    {
        return Mapper.Map<Public.DTO.v1.GroupWithData>(entity);
    }
    
    public AddGroup MapToAddGroup(Public.DTO.v1.AddGroup entity)
    {
        return Mapper.Map<AddGroup>(entity);
    }
    
    public v1.Identity.AppUser MapAppUser(AppUser entity)
    {
        return Mapper.Map<v1.Identity.AppUser>(entity);
    }
}