using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EventRepository : EFBaseRepository<Domain.Event, Event, ApplicationDbContext>, IEventRepository
{
    public EventRepository(ApplicationDbContext dataContext, IMapper<Domain.Event, Event> mapper) : base(dataContext, mapper)
    {
    }

    public virtual async Task<IEnumerable<Event>> AllPublicAsync(EventFilter? eventFilter, Guid userId)
    {
        if (eventFilter == null)
        {
            var asd = await RepositoryDbSet
                .Include(c => c.Colour)
                .Include(c => c.Location)
                    .ThenInclude(c => c!.Country)
                .Include(c => c.Activity)
                .Include(c => c.EventInGroups)
                .OrderBy(c => c.StartsAt)
                .Where(c => c.CreatorId != userId)
                .ToListAsync();
            return asd
                .Where(c => c.EventInGroups!.Count != 0)
                .Select(d => Mapper.Map(d)!);
        }
        
        var data = await RepositoryDbSet
            .Include(c => c.Colour)
            .Include(c => c.Location)
                .ThenInclude(c => c!.Country)
            .Include(c => c.Activity)
            .OrderBy(c => c.StartsAt)
            .Where(c => c.CreatorId != userId)
            .Where(c => c.EventInGroups!.Any(a => a.EventId == c.Id))
            .Where(c => eventFilter.ActivityId == null || eventFilter.ActivityId == c.ActivityId )
            .Where(c => eventFilter.CountryId == null || eventFilter.CountryId == c.Location!.CountryId )
            .Where(c => eventFilter.TimeFrom == null || eventFilter.TimeFrom <= c.StartsAt )
            .Where(c => eventFilter.TimeTo == null || eventFilter.TimeTo >= c.EndsAt )
            .ToListAsync();
        return data.Select(d => Mapper.Map(d)!);
    }

    public override async Task<IEnumerable<Event>> AllAsync()
    {
        var data = await RepositoryDbSet
            .Include(c => c.Colour)
            .Include(c => c.Location)
                .ThenInclude(c => c!.Country)
            .Include(c => c.Activity)
            .Include(c => c.UsersEvent)
            .Include(c => c.EventInGroups)
            .OrderBy(c => c.StartsAt)
            .ToListAsync();
        return data.Select(d => Mapper.Map(d)!);
    }

    public override async Task<Event?> FindAsync(Guid id)
    {
        var data = await RepositoryDbSet
            .Include(c => c.Colour)
            .Include(c => c.Location)
                .ThenInclude(c => c!.Country)
            .Include(c => c.Activity)
            .Include(c => c.UsersEvent)
            .Include(c => c.EventInGroups)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return Mapper.Map(data);
    }

    public void DeleteEvents(IEnumerable<Guid> eventIds)
    {
        foreach (var eventId in eventIds)
        {
            RepositoryDbSet.RemoveRange(
                RepositoryDbSet
                    .AsNoTracking()
                    .Where(c => c.Id == eventId)
                    .Where(c => c.UsersEvent!.Count == 0)
                    .Where(c => c.EventInGroups!.Count == 0)
            );
        }
    }

    public override async Task<Event?> RemoveAsync(Guid id)
    {
        var res = await RepositoryDbSet
            // .Where(c => c.UsersEvent!.Count == 0)
            // .Where(c => c.EventInGroups!.Count == 0)
            .Include(c => c.UsersEvent)
            .Include(c => c.EventInGroups)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        if (res != null && res.UsersEvent!.Count == 0 && res.EventInGroups!.Count == 0)
        {
            return Remove(Mapper.Map(res)!);
        }
        return null;
    }
}