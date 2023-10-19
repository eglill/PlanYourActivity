using App.BLL.DTO;
using App.Contracts.DAL;
using Base.Contracts.DAL;

namespace App.Contracts.BLL;

public interface IEventService : IBaseRepository<GroupEvent>, IEventRepositoryCustom<GroupEvent>
{
    // add your custom service methods here
    Task<IEnumerable<GroupEvent>> AllPublicAsync(EventFilter? eventFilter, Guid userId);
    Task<IEnumerable<UserEvent>> AllUserEventsAsync(Guid userId);
    Task<IEnumerable<GroupEvent>?> AllGroupEventsAsync(Guid groupId, Guid userId);
    Task<GroupEvent?> AddGroupEvent(AddGroupEvent groupEvent, Guid userId);
    UserEvent? AddUserEvent(AddUserEvent userEvent, Guid userId);
    Task<UserEvent?> FindUserEventAsync(Guid eventId, Guid userId);
    Task<GroupEvent?> FindGroupEventAsync(Guid eventId, Guid userId);
    Task<bool> UpdateUserEvent(AddUserEvent userEvent, Guid eventId, Guid userId);
    Task<bool> UpdateGroupEvent(AddGroupEvent groupEvent, Guid eventId, Guid adminId);
    Task<bool> DeleteUserEvent(Guid eventId, Guid userId);
    Task<bool> DeleteGroupEvent(Guid eventId, Guid adminId);
    Task<bool> AddGroupEventToEvents(Guid eventId, Guid userId);

}