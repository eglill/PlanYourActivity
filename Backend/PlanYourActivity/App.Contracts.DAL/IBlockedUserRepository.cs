using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IBlockedUserRepository : IBaseRepository<BlockedUser>, IBlockedUserRepositoryCustom<BlockedUser>
{
    // add here custom methods for repo only
    Task<bool> IsUserBlocked(Guid blockedUserId, Guid blockerId);
}

public interface IBlockedUserRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    Task<IEnumerable<TEntity>?> AllAsync(Guid userId);
    Task<bool> RemoveAsync(Guid blockedUserId, Guid blockerId);
    
}