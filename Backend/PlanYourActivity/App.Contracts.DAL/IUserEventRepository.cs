using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserEventRepository : IBaseRepository<UserEvent>, IUserEventRepositoryCustom<UserEvent>
{
    // add here custom methods for repo only
    Task<IEnumerable<UserEvent>> AllAsync(Guid userId);
    Task<UserEvent?> FindAsync(Guid eventId, Guid userId);
    Task<bool> DeleteUpcomingEvents(Guid groupId, Guid userId);
    void DeleteUpcomingEvents(IEnumerable<Guid> eventIds);
    Task<bool> EventHaveUsers(Guid eventId);
    
}

public interface IUserEventRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}