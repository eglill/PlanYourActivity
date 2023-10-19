using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IEventRepository : IBaseRepository<Event>, IEventRepositoryCustom<Event>
{
    // add here custom methods for repo only
    void DeleteEvents(IEnumerable<Guid> eventIds);
    Task<IEnumerable<Event>> AllPublicAsync(EventFilter? eventFilter, Guid userId);
    // Task<bool> UpdateUserEvent(Event eventDal, Guid userId);
    // Task<bool> UpdateGroupEvent(AddGroupEvent groupEvent, Guid eventId, Guid adminId);
}

public interface IEventRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}