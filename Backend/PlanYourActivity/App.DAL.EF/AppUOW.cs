using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.DAL.EF.Mappers.Identity;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : EFBaseUOW<ApplicationDbContext>, IAppUOW
{
    private readonly AutoMapper.IMapper _mapper;
    public AppUOW(ApplicationDbContext dataContext, AutoMapper.IMapper mapper) : base(dataContext)
    {
        _mapper = mapper;
    }
    
    private IUserRepository? _userRepository;
    public IUserRepository UserRepository =>
        _userRepository ??= new UserRepository(UowDbContext, new AppUserMapper(_mapper));

    private IActivityRepository? _activityRepository;
    public IActivityRepository ActivityRepository =>
        _activityRepository ??= new ActivityRepository(UowDbContext, new ActivityMapper(_mapper));
    
    private IBlockedUserRepository? _blockedUserRepository;
    public IBlockedUserRepository BlockedUserRepository =>
        _blockedUserRepository ??= new BlockedUserRepository(UowDbContext, new BlockedUserMapper(_mapper));
    
    private IColourRepository? _colourRepository;
    public IColourRepository ColourRepository =>
        _colourRepository ??= new ColourRepository(UowDbContext, new ColourMapper(_mapper));
    
    private IConversationRepository? _conversationRepository;
    public IConversationRepository ConversationRepository =>
        _conversationRepository ??= new ConversationRepository(UowDbContext, new ConversationMapper(_mapper));
    
    private ICountryRepository? _countryRepository;
    public ICountryRepository CountryRepository  =>
        _countryRepository ??= new CountryRepository (UowDbContext, new CountryMapper(_mapper));
    
    private IEventInGroupRepository? _eventInGroupRepository;
    public IEventInGroupRepository EventInGroupRepository =>
        _eventInGroupRepository ??= new EventInGroupRepository(UowDbContext, new EventInGroupMapper(_mapper));
    
    private IEventRepository? _eventRepository;
    public IEventRepository EventRepository =>
        _eventRepository ??= new EventRepository(UowDbContext, new EventMapper(_mapper));

    private IFavoriteUserRepository? _favoriteUserRepository;
    public IFavoriteUserRepository FavoriteUserRepository =>
        _favoriteUserRepository ??= new FavoriteUserRepository(UowDbContext, new FavoriteUserMapper(_mapper));
    
    private IGenderRepository? _genderRepository;
    public IGenderRepository GenderRepository =>
        _genderRepository ??= new GenderRepository(UowDbContext, new GenderMapper(_mapper));
    
    private IGroupRepository? _groupRepository;
    public IGroupRepository GroupRepository =>
        _groupRepository ??= new GroupRepository(UowDbContext, new GroupMapper(_mapper));
    
    private IInviteToGroupRepository? _inviteToGroupRepository;
    public IInviteToGroupRepository InviteToGroupRepository =>
        _inviteToGroupRepository ??= new InviteToGroupRepository(UowDbContext, new InviteToGroupMapper(_mapper));
    
    private ILocationRepository? _locationRepository;
    public ILocationRepository LocationRepository =>
        _locationRepository ??= new LocationRepository(UowDbContext, new LocationMapper(_mapper));
    
    private IMessageRepository? _messageRepository;
    public IMessageRepository MessageRepository =>
        _messageRepository ??= new MessageRepository(UowDbContext, new MessageMapper(_mapper));
    
    private IPollOptionRepository? _pollOptionRepository;
    public IPollOptionRepository PollOptionRepository =>
        _pollOptionRepository ??= new PollOptionRepository(UowDbContext, new PollOptionMapper(_mapper));
    
    private IPollQuestionRepository? _pollQuestionRepository;
    public IPollQuestionRepository PollQuestionRepository =>
        _pollQuestionRepository ??= new PollQuestionRepository(UowDbContext, new PollQuestionMapper(_mapper));
    
    private IUserEventRepository? _userEventRepository;
    public IUserEventRepository UserEventRepository =>
        _userEventRepository ??= new UserEventRepository(UowDbContext, new UserEventMapper(_mapper));
    
    private IUserInGroupRepository? _userInGroupRepository;
    public IUserInGroupRepository UserInGroupRepository =>
        _userInGroupRepository ??= new UserInGroupRepository(UowDbContext, new UserInGroupMapper(_mapper));
    
    private IUserOptionChoiceRepository? _userOptionChoiceRepository;
    public IUserOptionChoiceRepository UserOptionChoiceRepository =>
        _userOptionChoiceRepository ??= new UserOptionChoiceRepository(UowDbContext, new UserOptionChoiceMapper(_mapper));
}
