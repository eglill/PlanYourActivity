using App.DAL.DTO;
using App.DAL.DTO.identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IGroupRepository : IBaseRepository<Group>, IGroupRepositoryCustom<Group>
{
    // add here custom methods for repo only
    Task<IEnumerable<Group>> AllAsync(AppUser user);
    Task<bool> IsItPossibleToJoin(Guid groupId, AppUser user);
    Task<IEnumerable<Group>> AllGroupInvitations(Guid userId);
    Task<bool> Update(Group group, Guid adminId);

}

public interface IGroupRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    Task<bool> Delete(Guid groupId, Guid adminId);
    Task<bool> IsUserAdmin(Guid groupId, Guid adminId);
}