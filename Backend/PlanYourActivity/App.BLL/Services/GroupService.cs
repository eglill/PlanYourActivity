using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.BLL.Mappers;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DAL.DTO;
using AutoMapper;
using Base.BLL;
using Base.Conteracts;
using Base.DAL;
using Group = App.BLL.DTO.Group;


namespace App.BLL.Services;

public class GroupService : BaseEntityService<DAL.DTO.Group, Group, IGroupRepository>, IGroupService
{
    protected IAppUOW Uow;
    private GroupMapper _mapper;

    public GroupService(IAppUOW uow, IMapper mapper)
        : base(uow.GroupRepository, new GroupMapper(mapper))
    {
        Uow = uow;
        _mapper = new GroupMapper(mapper);
    }

    public async Task<IEnumerable<Group>> AllAsync(Guid userId)
    {
        var user = await Uow.UserRepository.GetUser(userId);
        if (user == null)
        {
            return new List<Group>();
        }

        var data = await Uow.GroupRepository.AllAsync(new DAL.DTO.identity.AppUser{
            Id = user.Id,
            GenderId = user.GenderId,
            BirthDate = user.BirthDate
        });
        return data.Select(d => Mapper.Map(d)!);
    }

    public async Task<GroupWithData?> FindGroupAsync(Guid groupId, Guid userId)
    {
        var group = await Uow.UserInGroupRepository.FindAsync(groupId, userId);
        return group == null ? null : _mapper.MapToGroupWithData(group.Group!);
    }

    public async Task<IEnumerable<Group>> FindInvites(Guid userId)
    {
        return (await Uow.GroupRepository.AllGroupInvitations(userId)).Select(c => Mapper.Map(c)!);
    }

    public async Task<IEnumerable<Group>> UserGroups(Guid userId)
    {
        return (await Uow.UserInGroupRepository.UserGroups(userId)).Select(s => Mapper.Map(s.Group)!);
    }

    public async Task<bool> JoinGroup(Guid groupId, Guid userId)
    {
        var possibleToJoin = await IsItPossibleToJoin(groupId, userId);
        if (possibleToJoin)
        {
            Uow.UserInGroupRepository.Add(new UserInGroup{AppUserId = userId, GroupId = groupId});
            return true;
        }

        return false;
    }

    public async Task<bool> LeaveGroup(Guid groupId, Guid userId)
    {
        var leaved = await Uow.UserInGroupRepository.LeaveGroup(groupId, userId);

        if (leaved)
        {
            return await Uow.UserEventRepository.DeleteUpcomingEvents(groupId, userId);
        }
        return false;
    }

    public async Task<bool> IsItPossibleToJoin(Guid groupId, Guid userId)
    {
        var user = await Uow.UserRepository.GetUser(userId);
        if (user == null)
        {
            return false;
        }
        
        var invitations = await Uow.GroupRepository.AllGroupInvitations(userId);
        if (invitations.Any(invitation => invitation.Id == groupId))
        {
            return true;
        }
        
        return await Uow.GroupRepository.IsItPossibleToJoin(groupId, user);
    }

    public async Task<bool> KickUser(Guid groupId, Guid userId, Guid adminId)
    {
        return await Uow.UserInGroupRepository.KickUser(groupId, userId, adminId);
    }

    public async Task<bool> InviteUser(Guid groupId, Guid userId, Guid adminId)
    {
        var isAdmin = await Uow.GroupRepository.IsUserAdmin(groupId, adminId);
        if (isAdmin)
        {
            Uow.InviteToGroupRepository.Add(new InviteToGroup {AppUserId = userId, GroupId = groupId});
            return true;
        }
        return false;
    }

    public async Task<bool> Update(AddGroup group, Guid adminId)
    {
       return await Uow.GroupRepository.Update(_mapper.MapToGroup(group), adminId);
       
    }

    public Group Add(AddGroup group)
    {
        var newGroup = Uow.GroupRepository.Add(_mapper.MapToGroup(group));
        Uow.UserInGroupRepository.Add(new UserInGroup {AppUserId = newGroup.AdminId, GroupId = newGroup.Id});
        Uow.ConversationRepository.Add(new DAL.DTO.Conversation {GroupId = newGroup.Id});
        return Mapper.Map(newGroup)!;
    }

    public async Task<IEnumerable<AppUser>?> UsersInGroup(Guid groupId, Guid userId)
    {
        var isUserInGroup = await Uow.UserInGroupRepository.IsUserInGroup(groupId, userId);
        if (isUserInGroup)
        {
            var res = await Uow.UserInGroupRepository.UsersInGroup(groupId, userId);
            return res.Select(m => _mapper.MapToAppUser(m));
        }

        return null;
    }

    public async Task<bool> Delete(Guid groupId, Guid adminId)
    {
        var isAdmin = await Uow.GroupRepository.IsUserAdmin(groupId, adminId);
        if (isAdmin == false)
        {
            return false;
        }

        Uow.UserOptionChoiceRepository.DeleteAllUserChoicesInGroup(groupId);
        Uow.PollOptionRepository.DeleteAllPollOptionsInGroup(groupId);
        Uow.PollQuestionRepository.DeleteAllPollQuestionsInGroup(groupId);
        Uow.UserInGroupRepository.RemoveAllUsersFromGroup(groupId);
        Uow.InviteToGroupRepository.DeleteAllInvitesToGroup(groupId);
        Uow.MessageRepository.DeleteAllMessagesInGroup(groupId);
        Uow.ConversationRepository.DeleteGroupConversation(groupId);
        var deletedEventIds = await Uow.EventInGroupRepository.DeleteEventsInGroup(groupId);
        Uow.UserEventRepository.DeleteUpcomingEvents(deletedEventIds);
        Uow.EventRepository.DeleteEvents(deletedEventIds);
        return await Uow.GroupRepository.Delete(groupId, adminId);
    }

    public async Task<bool> IsUserAdmin(Guid groupId, Guid adminId)
    {
        return await Uow.GroupRepository.IsUserAdmin(groupId, adminId);
    }
}