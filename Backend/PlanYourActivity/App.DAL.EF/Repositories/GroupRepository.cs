using App.Contracts.DAL;
using App.DAL.DTO;
using App.DAL.DTO.identity;
using Base.Conteracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class GroupRepository : EFBaseRepository<Domain.Group, Group, ApplicationDbContext>, IGroupRepository
{
    public GroupRepository(ApplicationDbContext dataContext, IMapper<Domain.Group, Group> mapper) : base(dataContext, mapper)
    {
    }
    
    public async Task<IEnumerable<Group>> AllAsync(AppUser user)
    {
        var today = DateTime.Today;
        var age = today.Year - user.BirthDate.Year;

        var data = await RepositoryDbSet
            .Include(c => c.GroupGender)
            .Include(c => c.Admin)
                .ThenInclude(c => c!.BlockedUsers!.Where(a => a.BlockedAppUserId == user.Id))
            .Include(c => c.UsersInGroup!.Where(a => a.LeftAt == null))
            .Where(c => c.JoiningLocked == false)
            .Where(c => c.GroupGender == null || c.GroupGender.Id == user.GenderId)
            .Where(c => c.MaxAge == null || c.MaxAge >= age)
            .Where(c => c.MinAge == null || c.MinAge <= age)
            .Where(c => c.Private == false)
            .ToListAsync();
        
        
        return data
            .Where(c => c.MaxParticipants > c.UsersInGroup!.Count)
            .Where(c => false == c.UsersInGroup!.Any(a => a.AppUserId == user.Id))
            .Where(c => c.Admin!.BlockedUsers!.Count == 0)
            .Select(d => Mapper.Map(d)!);
    }
    
    public async Task<bool> IsItPossibleToJoin(Guid groupId, AppUser user)
    {
        var today = DateTime.Today;
        var age = today.Year - user.BirthDate.Year;

        var data = await RepositoryDbSet
            .Where(c => c.Id == groupId)
            .Where(c => c.MaxParticipants > c.UsersInGroup!.Count(a => a.LeftAt == null))
            .Where(c => c.JoiningLocked == false)
            .Where(c => c.Private == false)
            .Where(c => c.GroupGender == null || c.GroupGender.Id == user.GenderId)
            .Where(c => c.MaxAge == null || c.MaxAge >= age)
            .Where(c => c.MinAge == null || c.MinAge <= age)
            .Where(c => false == c.Admin!.BlockedUsers!
                .Any(a => a.BlockerId == c.AdminId && a.BlockedAppUserId == user.Id))
            .FirstOrDefaultAsync();

        return data != null;
    }

    public async Task<IEnumerable<Group>> AllGroupInvitations(Guid userId)
    {
        var data = await RepositoryDbSet
            .Include(c => c.GroupGender)
            .Where(c => c.InvitesToGroup!.Any(a => a.AppUserId == userId))
            .ToListAsync();
        return data.Select(a => Mapper.Map(a)!);
    }

    public async Task<bool> IsUserAdmin(Guid groupId, Guid adminId)
    {
        return await RepositoryDbSet
            .AnyAsync(a => a.AdminId == adminId && a.Id == groupId);
    }

    public virtual async Task<bool> Update(Group group, Guid adminId)
    {
        var anyMatchingGroup = await RepositoryDbSet
            .AnyAsync(c => c.AdminId == adminId && c.Id == group.Id);

        if (anyMatchingGroup)
        {
            RepositoryDbSet.Update(Mapper.Map(group)!);
            return true;
        }

        return false;
    }

    public virtual async Task<bool> Delete(Guid groupId, Guid adminId)
    {
        var group = await RepositoryDbSet
            .Where(c => c.Id == groupId && c.AdminId == adminId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (group == null)
        {
            return false;
        }
        Remove(Mapper.Map(group)!);
        return true;
    }
}