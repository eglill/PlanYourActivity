using App.BLL.DTO;
using App.BLL.Mappers;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DAL.DTO;
using Base.BLL;
using EventFilter = App.BLL.DTO.EventFilter;
using UserEvent = App.BLL.DTO.UserEvent;

namespace App.BLL.Services;

public class EventService : BaseEntityService<DAL.DTO.Event, GroupEvent, IEventRepository>, IEventService
{
    protected IAppUOW Uow;
    private EventMapper _mapper;

    public EventService(IAppUOW uow, EventMapper mapper)
        : base(uow.EventRepository, mapper)
    {
        Uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GroupEvent>> AllPublicAsync(EventFilter? eventFilter, Guid userId)
    {
        var user = await Uow.UserRepository.GetUser(userId);
        if (user == null)
        {
            return new List<GroupEvent>();
        }
        var res = new List<GroupEvent>();
        var events = await Uow.EventRepository.AllPublicAsync(_mapper.MapEventFilter(eventFilter), userId);
        foreach (var e in events)
        {
            foreach (var eventInGroup in e.EventInGroups!)
            {
                var canJoin = await Uow.GroupRepository.IsItPossibleToJoin(eventInGroup.GroupId, user);
                if (canJoin)
                {
                    res.Add(Mapper.Map(e)!);
                }
            }

        }
        return res;
    }

    public async Task<IEnumerable<UserEvent>> AllUserEventsAsync(Guid userId)
    {
        return (await Uow.UserEventRepository.AllAsync(userId)).Select(
            c => { 
                var d = _mapper.MapToUserEvent(c.Event!);
                d.AvailableForGroupEvent = c.AvailableForGroupEvent;
                d.Editable = c.Event!.CreatorId == userId;

                return d;
            });
    }

    public async Task<IEnumerable<GroupEvent>?> AllGroupEventsAsync(Guid groupId, Guid userId)
    {
        var userInGroup = await Uow.UserInGroupRepository.IsUserInGroup(groupId, userId);
        if (userInGroup)
        {
            return (await Uow.EventInGroupRepository.AllAsync(groupId)).Select(c => Mapper.Map(c.Event)!);
        }
        return null;
    }

    public async Task<GroupEvent?> AddGroupEvent(AddGroupEvent groupEvent, Guid userId)
    {
        await Uow.GroupRepository.IsUserAdmin(groupEvent.GroupId, userId);
        
        var dalEvent = _mapper.MapToEvent(groupEvent);
        dalEvent.CreatorId = userId;

        var newEvent = Uow.EventRepository.Add(dalEvent);
        Uow.EventInGroupRepository.Add(new EventInGroup {GroupId = groupEvent.GroupId, EventId = newEvent.Id});
        return Mapper.Map(newEvent);
        
    }

    public UserEvent AddUserEvent(AddUserEvent addUserEvent, Guid userId)
    {
        var dalEvent = _mapper.MapToEvent(addUserEvent);
        var newEvent = Uow.EventRepository.Add(dalEvent);
        var userEvent = new DAL.DTO.UserEvent
        {
            AppUserId = userId,
            EventId = newEvent.Id,
            AvailableForGroupEvent = addUserEvent.AvailableForGroupEvent
        };
        Uow.UserEventRepository.Add(userEvent);
        return _mapper.MapToUserEvent(newEvent);
    }

    public async Task<UserEvent?> FindUserEventAsync(Guid eventId, Guid userId)
    {
        var userEvent = await Uow.UserEventRepository.FindAsync(eventId, userId);
        
        if (userEvent == null)
        {
            return null;
        }

        var e = _mapper.MapToUserEvent(userEvent.Event!);
        e.AvailableForGroupEvent = userEvent.AvailableForGroupEvent;
        e.Editable = userEvent.Event!.CreatorId == userId;
        return e;
    }
    
    public async Task<GroupEvent?> FindGroupEventAsync(Guid eventId, Guid userId)
    {
        var groupEvent = await Uow.EventInGroupRepository.FindAsync(eventId, userId);
        if (groupEvent != null)
        {
            var userInGroup = await Uow.UserInGroupRepository.IsUserInGroup(groupEvent.GroupId, userId);
            if (userInGroup)
            {
                return Mapper.Map(groupEvent.Event);
            }
        }
        return null;
    }

    public async Task<bool> UpdateUserEvent(AddUserEvent userEvent, Guid eventId, Guid userId)
    {
        var isCreator = (await Uow.EventRepository.FindAsync(eventId))?.CreatorId == userId;
        if (isCreator)
        {
            var userEventDal = await Uow.UserEventRepository.FindAsync(eventId, userId);
            userEventDal!.AvailableForGroupEvent = userEvent.AvailableForGroupEvent;
            Uow.UserEventRepository.Update(userEventDal);

            var eventDal = _mapper.MapToEvent(userEvent);
            eventDal.Id = eventId;
            eventDal.CreatorId = userId;
            Uow.EventRepository.Update(eventDal);
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateGroupEvent(AddGroupEvent groupEvent, Guid eventId, Guid adminId)
    {
        var isAdmin = await Uow.GroupRepository.IsUserAdmin(groupEvent.GroupId, adminId);
        if (isAdmin == false)
        {
            return false;
        }

        var eventDal = _mapper.MapToEvent(groupEvent);
        eventDal.Id = eventId;
        eventDal.CreatorId = adminId;
        Uow.EventRepository.Update(eventDal);
        return true;
    }

    public async Task<bool> DeleteUserEvent(Guid eventId, Guid userId)
    {
        var userEvent = await Uow.UserEventRepository.FindAsync(eventId, userId);
        if (userEvent == null)
        {
            return false;
        }

        Uow.UserEventRepository.Remove(new DAL.DTO.UserEvent{Id = userEvent.Id});
        await Uow.EventRepository.RemoveAsync(eventId);
        return true;
    }

    public async Task<bool> DeleteGroupEvent(Guid eventId, Guid adminId)
    {
        var groupEvent = await Uow.EventRepository.FindAsync(eventId);
        var eventInGroup = await Uow.EventInGroupRepository.FindAsync(eventId, adminId);
        if (groupEvent == null || groupEvent.CreatorId != adminId || eventInGroup == null)
        {
            return false;
        }
        
        Uow.UserEventRepository.DeleteUpcomingEvents(new[] {eventId});
        var eventHaveUsers = await Uow.UserEventRepository.EventHaveUsers(eventId);
        Uow.EventInGroupRepository.Remove(new EventInGroup{Id = eventInGroup.Id});
        if (!eventHaveUsers)
        {
            Uow.EventRepository.Remove(new Event {Id = groupEvent.Id});
        }
        return true;
    }

    public async Task<bool> AddGroupEventToEvents(Guid eventId, Guid userId)
    {
        var eventInGroup = await Uow.EventInGroupRepository.FindAsync(eventId, userId);
        if (eventInGroup != null)
        {
            Console.WriteLine("Event in group: not null");
            var userInGroup = await Uow.UserInGroupRepository.IsUserInGroup(eventInGroup.GroupId, userId);
            var alreadyAdded = await Uow.UserEventRepository.FindAsync(eventId, userId) != null;
            Console.WriteLine("userInGroup: " + userInGroup.ToString());
            Console.WriteLine("alreadyAdded: " + alreadyAdded.ToString());

            if (userInGroup && !alreadyAdded)
            {
                var asd = Uow.UserEventRepository.Add(new DAL.DTO.UserEvent
                {
                    EventId = eventId,
                    AppUserId = userId,
                    AvailableForGroupEvent = false
                });
                Console.Write("Added: " + asd.Id);
                return true;
            }
        }

        return false;
    }
}