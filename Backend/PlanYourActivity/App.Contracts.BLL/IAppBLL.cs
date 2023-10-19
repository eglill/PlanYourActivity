using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBaseBLL
{
    IGenderService GenderService { get; }
    ICountryService CountryService { get; }
    IEventService EventService { get; }
    IGroupService GroupService { get; }
    IBlockedUserService BlockedUserService { get; }
    IUserService UserService { get; }
    IConversationService ConversationService { get; }
    IColourService ColourService { get; }
    IActivityService ActivityService { get; }
}