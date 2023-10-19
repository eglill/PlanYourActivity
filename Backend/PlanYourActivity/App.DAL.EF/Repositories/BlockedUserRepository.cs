using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class BlockedUserRepository : EFBaseRepository<Domain.BlockedUser, BlockedUser, ApplicationDbContext>, IBlockedUserRepository
{
    public BlockedUserRepository(ApplicationDbContext dataContext, IMapper<Domain.BlockedUser, BlockedUser> mapper) : base(dataContext, mapper)
    {
    }

    public async Task<bool> IsUserBlocked(Guid blockedUserId, Guid blockerId)
    {
        var data = await RepositoryDbSet
            .Where(c => c.BlockerId == blockerId && c.BlockedAppUserId == blockedUserId)
            .FirstOrDefaultAsync();
        return data != null;
    }

    public async Task<IEnumerable<BlockedUser>?> AllAsync(Guid userId)
    {
        var data = await RepositoryDbSet
            .Include(c => c.BlockedAppUser)
            .Where(c => c.BlockerId == userId)
            .ToListAsync();
        return data.Select(s => Mapper.Map(s)!);
    }
    
    public async Task<bool> RemoveAsync(Guid blockedUserId, Guid blockerId)
    {
        var data = await RepositoryDbSet
            .Where(c => c.BlockerId == blockerId && c.BlockedAppUserId == blockedUserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        
        if (data != null)
        {
            var asd = Remove(Mapper.Map(data)!);
            return true;
        }
        
        return false;
    }
}