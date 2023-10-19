using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IEventInGroupRepository : IBaseRepository<EventInGroup>, IEventInGroupRepositoryCustom<EventInGroup>
{
    // add here custom methods for repo only
    Task<IEnumerable<EventInGroup>> AllAsync(Guid groupId);
    Task<EventInGroup?> FindAsync(Guid eventId, Guid userId);
    Task<List<Guid>> DeleteEventsInGroup(Guid groupId);

}

public interface IEventInGroupRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}