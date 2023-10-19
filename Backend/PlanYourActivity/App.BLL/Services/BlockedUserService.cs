using App.BLL.DTO;
using App.Contracts.BLL;
using App.Contracts.DAL;
using Base.BLL;
using Base.Conteracts;

namespace App.BLL.Services;

public class BlockedUserService : BaseEntityService<DAL.DTO.BlockedUser, BlockedUser, IBlockedUserRepository>, IBlockedUserService
{
    protected IAppUOW Uow;

    public BlockedUserService(IAppUOW uow, IMapper<DAL.DTO.BlockedUser, BlockedUser> mapper)
        : base(uow.BlockedUserRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<BlockedUser>?> AllAsync(Guid userId)
    {
        var data = await Uow.BlockedUserRepository.AllAsync(userId);
        return data?.Select(s => Mapper.Map(s)!);
    }
    
    public async Task<bool> BlockUser(Guid blockedUserId, Guid blockerId)
    {
        var data = await Uow.BlockedUserRepository.IsUserBlocked(blockedUserId, blockerId);
        
        
        if (data)
        {
            return false;
        }
        
        Uow.BlockedUserRepository.Add(new DAL.DTO.BlockedUser
            {BlockerId = blockerId, BlockedAppUserId = blockedUserId, CreatedAt = DateTime.UtcNow});
        return true;
        
    }

    public async Task<bool> RemoveAsync(Guid blockedUserId, Guid blockerId)
    {
        return await Uow.BlockedUserRepository.RemoveAsync(blockedUserId, blockerId);
    }
}