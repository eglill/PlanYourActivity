using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IInviteToGroupRepository : IBaseRepository<InviteToGroup>, IInviteToGroupRepositoryCustom<InviteToGroup>
{
    // add here custom methods for repo only
    void DeleteAllInvitesToGroup(Guid groupId);
}

public interface IInviteToGroupRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}