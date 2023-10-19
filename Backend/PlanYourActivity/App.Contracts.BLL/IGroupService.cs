using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.DAL;
using Base.Contracts.DAL;

namespace App.Contracts.BLL;

public interface IGroupService : IBaseRepository<Group>, IGroupRepositoryCustom<Group>
{
    // add your custom service methods here
    Task<IEnumerable<Group>> AllAsync(Guid userId);
    Task<GroupWithData?> FindGroupAsync(Guid groupId, Guid userId);
    Task<IEnumerable<Group>> FindInvites(Guid userId);
    Task<IEnumerable<Group>> UserGroups(Guid userId);
    Task<bool> JoinGroup(Guid groupId, Guid userId);
    Task<bool> LeaveGroup(Guid groupId, Guid userId);
    Task<bool> IsItPossibleToJoin(Guid groupId, Guid userId);
    Task<bool> KickUser(Guid groupId, Guid userId, Guid adminId);
    Task<bool> InviteUser(Guid groupId, Guid userId, Guid adminId);
    Task<bool> Update(AddGroup group, Guid adminId);
    Group Add(AddGroup group);
    Task<IEnumerable<AppUser>?> UsersInGroup(Guid groupId, Guid userId);
}