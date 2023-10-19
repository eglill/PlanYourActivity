using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace App.DAL.EF.Repositories;

public class UserEventRepository : EFBaseRepository<Domain.UserEvent, UserEvent, ApplicationDbContext>, IUserEventRepository
{
    public UserEventRepository(ApplicationDbContext dataContext, IMapper<Domain.UserEvent, UserEvent> mapper) : base(dataContext, mapper)
    {
    }
    
    public virtual async Task<IEnumerable<UserEvent>> AllAsync(Guid userId)
    {
        var data = await RepositoryDbSet
            .Include(c => c.Event)
            .Include(c => c.Event!.Activity)
            .Include(c => c.Event!.Colour)
            .Include(c => c.Event!.Location)
            .Include(c => c.Event!.Location!.Country)
            .Where(c => c.AppUserId == userId)
            .ToListAsync();
        return data.Select(c => Mapper.Map(c)!);
    }

    public async Task<UserEvent?> FindAsync(Guid eventId, Guid userId)
    {
        var data = await RepositoryDbSet
            .Include(c => c.Event)
            .Include(c => c.Event!.Activity)
            .Include(c => c.Event!.Colour)
            .Include(c => c.Event!.Location)
            .Include(c => c.Event!.Location!.Country)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.AppUserId == userId && c.EventId == eventId);
        return Mapper.Map(data);
    }
    
    public async Task<bool> EventHaveUsers(Guid eventId)
    {
        var count = await RepositoryDbSet
            .Where(c => c.EventId == eventId)
            .CountAsync();

        return count != 0;
    }
    
    public async Task<bool> DeleteUpcomingEvents(Guid groupId, Guid userId)
    {
        await RepositoryDbSet
            .Where(c => c.AppUserId == userId)
            .Where(c => c.Event!.EventInGroups!.Any(a => a.GroupId == groupId))
            .Where(c => c.Event!.StartsAt > DateTime.UtcNow)
            .ExecuteDeleteAsync();
        return true;
    }

    public void DeleteUpcomingEvents(IEnumerable<Guid> eventIds)
    {
        foreach (var eventId in eventIds)
        {
            RepositoryDbSet.RemoveRange(
                RepositoryDbSet
                    .Where(c => c.EventId == eventId)
                    .Where(c => c.Event!.StartsAt > DateTime.UtcNow)
                );
        }
    }
}