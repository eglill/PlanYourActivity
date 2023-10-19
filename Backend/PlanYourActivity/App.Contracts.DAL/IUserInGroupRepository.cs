using App.DAL.DTO;
using App.DAL.DTO.identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserInGroupRepository : IBaseRepository<UserInGroup>, IUserInGroupRepositoryCustom<UserInGroup>
{
    // add here custom methods for repo only
    Task<bool> IsUserInGroup(Guid groupId, Guid userId);
    Task<IEnumerable<UserInGroup>> UserGroups(Guid userId);
    Task<UserInGroup?> FindAsync(Guid groupId, Guid userId);
    Task<bool> LeaveGroup(Guid groupId, Guid userId);
    Task<bool> KickUser(Guid groupId, Guid userId, Guid adminId);
    void RemoveAllUsersFromGroup(Guid groupId);
    Task<IEnumerable<AppUser>> UsersInGroup(Guid groupId, Guid userId);
}

public interface IUserInGroupRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}