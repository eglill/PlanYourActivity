using App.BLL.DTO;
using App.Contracts.DAL;
using Base.Contracts.DAL;

namespace App.Contracts.BLL;

public interface IBlockedUserService : IBaseRepository<BlockedUser>, IBlockedUserRepositoryCustom<BlockedUser>
{
    // add your custom service methods here
    Task<bool> BlockUser(Guid blockedUserId, Guid blockerId);
}