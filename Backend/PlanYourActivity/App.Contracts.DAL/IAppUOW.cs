using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUOW : IBaseUOW
{
    // list your repositories here
    IUserRepository UserRepository { get; }
    
    IActivityRepository ActivityRepository { get; }
    IBlockedUserRepository BlockedUserRepository { get; }
    IColourRepository ColourRepository { get; }
    IConversationRepository ConversationRepository { get; }
    ICountryRepository CountryRepository { get; }
    IEventInGroupRepository EventInGroupRepository { get; }
    IEventRepository EventRepository { get; }
    IFavoriteUserRepository FavoriteUserRepository { get; }
    IGenderRepository GenderRepository { get; }
    IGroupRepository GroupRepository { get; }
    IInviteToGroupRepository InviteToGroupRepository { get; }
    ILocationRepository LocationRepository { get; }
    IMessageRepository MessageRepository { get; }
    IPollOptionRepository PollOptionRepository { get; }
    IPollQuestionRepository PollQuestionRepository { get; }
    IUserEventRepository UserEventRepository { get; }
    IUserInGroupRepository UserInGroupRepository { get; }
    IUserOptionChoiceRepository UserOptionChoiceRepository { get; }
}
