using App.Contracts.DAL;
using App.DAL.DTO;
using App.DAL.DTO.identity;
using App.DAL.EF.Mappers;
using Base.Conteracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserInGroupRepository : EFBaseRepository<Domain.UserInGroup, UserInGroup, ApplicationDbContext>, IUserInGroupRepository
{
    private UserInGroupMapper _mapper;
    
    public UserInGroupRepository(ApplicationDbContext dataContext, UserInGroupMapper mapper) : base(dataContext, mapper)
    {
        _mapper = mapper;
        
    }

    public virtual async Task<bool> IsUserInGroup(Guid groupId, Guid userId)
    {
        var data = await RepositoryDbSet
            .Where(c => c.GroupId == groupId && c.AppUserId == userId)
            .Where(c => !c.LeftAt.HasValue)
            .FirstOrDefaultAsync();
        return data != null;
    }

    public async Task<IEnumerable<UserInGroup>> UserGroups(Guid userId)
    {
        var data = await RepositoryDbSet
            .Where(c => c.AppUserId == userId)
            .Where(c => !c.LeftAt.HasValue)
            .Include(c => c.Group)
            .ThenInclude(c => c!.GroupGender)
            .ToListAsync();
        return data.Select(s => Mapper.Map(s)!);
    }
    
    public async Task<UserInGroup?> FindAsync(Guid groupId, Guid userId)
    {
        var data = await RepositoryDbSet
            .Include(c => c.Group)
                .ThenInclude(c => c!.Admin)
            .Include(c => c.Group)
                .ThenInclude(c => c!.GroupGender)
            .Include(c => c.Group)
                .ThenInclude(c => c!.EventsInGroup)
            .Include(c => c.Group)
                .ThenInclude(c => c!.UsersInGroup!.Where(a => !a.LeftAt.HasValue))
            .Where(c => !c.LeftAt.HasValue)
            .FirstOrDefaultAsync(c => c.GroupId == groupId && c.AppUserId == userId);

        return Mapper.Map(data);
    }

    public async Task<bool> LeaveGroup(Guid groupId, Guid userId)
    {
        var data = await RepositoryDbSet
            .Where(c => c.GroupId == groupId && c.AppUserId == userId)
            .Where(c => !c.LeftAt.HasValue)
            .FirstOrDefaultAsync();
        
        if (data == null)
        {
            return false;
        }
        
        data.LeftAt = DateTime.UtcNow;
        RepositoryDbSet.Update(data);
        return true;
    }

    public async Task<bool> KickUser(Guid groupId, Guid userId, Guid adminId)
    {
        var data = await RepositoryDbSet
            .Where(c => c.GroupId == groupId)
            .Where(c => c.AppUserId == userId)
            .Where(c => !c.LeftAt.HasValue)
            .Where(c => c.Group!.AdminId == adminId)
            .FirstOrDefaultAsync();
        
        if (data == null)
        {
            return false;
        }
        
        data.LeftAt = DateTime.UtcNow;
        RepositoryDbSet.Update(data);
        return true;
    }

    public void RemoveAllUsersFromGroup(Guid groupId)
    {
        RepositoryDbSet.RemoveRange(RepositoryDbSet.Where(c => c.GroupId == groupId));
    }

    public async Task<IEnumerable<AppUser>> UsersInGroup(Guid groupId, Guid userId)
    {
        var data = await RepositoryDbSet.Include(g => g.AppUser)
            .Where(c => c.GroupId == groupId)
            .Where(c => false == c.LeftAt.HasValue)
            .ToListAsync();
        return data.Select(s => _mapper.MapAppUser(s.AppUser!));
    }
}