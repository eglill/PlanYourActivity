using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EventInGroupRepository : EFBaseRepository<Domain.EventInGroup, EventInGroup, ApplicationDbContext>, IEventInGroupRepository
{
    public EventInGroupRepository(ApplicationDbContext dataContext, IMapper<Domain.EventInGroup, EventInGroup> mapper) : base(dataContext, mapper)
    {
    }

    public async Task<IEnumerable<EventInGroup>> AllAsync(Guid groupId)
    {
        var data = await RepositoryDbSet
            .Include(c => c.Event)
                .ThenInclude(c => c!.Activity)
            .Include(c => c.Event)
                .ThenInclude(c => c!.Colour)
            .Include(c => c.Event)
                .ThenInclude(c => c!.Location)
            .Include(c => c.Event)
                .ThenInclude(c => c!.Location)
                    .ThenInclude(c => c!.Country)
            .Where(c => c.GroupId == groupId)
            .ToListAsync();
        return data.Select(c => Mapper.Map(c)!);
    }
    
    public async Task<EventInGroup?> FindAsync(Guid eventId, Guid userId)
    {
        // TODO check if Group must be included
        var data = await RepositoryDbSet
            .Include(c => c.Group)
            .Include(c => c.Event)
                .ThenInclude(c => c!.Activity)
            .Include(c => c.Event)
                .ThenInclude(c => c!.Colour)
            .Include(c => c.Event)
                .ThenInclude(c => c!.Location)
            .Include(c => c.Event)
                .ThenInclude(c => c!.Location)
                    .ThenInclude(c => c!.Country)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.EventId == eventId);
        return Mapper.Map(data);
    }

    public async Task<List<Guid>> DeleteEventsInGroup(Guid groupId)
    {
        var eventIds = await RepositoryDbSet
            .Where(c => c.GroupId == groupId)
            .AsNoTracking()
            .Select(c => c.EventId)
            .ToListAsync();
        
        RepositoryDbSet.RemoveRange(RepositoryDbSet.Where(c => c.GroupId == groupId));

        return eventIds;
    }
}