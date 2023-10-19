using App.BLL.Mappers;
using App.BLL.Services;
using AutoMapper;
using App.Contracts.BLL;
using App.Contracts.DAL;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    protected IAppUOW Uow;
    protected readonly AutoMapper.IMapper _mapper;

    public AppBLL(IAppUOW uow, IMapper mapper)
    {
        Uow = uow;
        _mapper = mapper;
    }
    
    public override async Task<int> SaveChangesAsync()
    {
        return await Uow.SaveChangesAsync();
    }

    private IGenderService? _gender;
    public IGenderService GenderService =>
        _gender ??= new GenderService(Uow, new GenderMapper(_mapper));
    
    private IEventService? _event;
    public IEventService EventService =>
        _event ??= new EventService(Uow, new EventMapper(_mapper));
    
    private IGroupService? _group;
    public IGroupService GroupService =>
        _group ??= new GroupService(Uow, _mapper);
    
    private IBlockedUserService? _blockedUser;
    public IBlockedUserService BlockedUserService =>
        _blockedUser ??= new BlockedUserService(Uow, new BlockedUserMapper(_mapper));
    
    private IUserService? _user;
    public IUserService UserService =>
        _user ??= new UserService(Uow, new AppUserMapper(_mapper));
    
    private IConversationService? _conversation;
    public IConversationService ConversationService =>
        _conversation ??= new ConversationService(Uow, new ConversationMapper(_mapper));
    
    private ICountryService? _country;
    public ICountryService CountryService =>
        _country ??= new CountryService(Uow, new CountryMapper(_mapper));
    
    private IColourService? _colour;
    public IColourService ColourService =>
        _colour ??= new ColourService(Uow, new ColourMapper(_mapper));
    
    private IActivityService? _activity;
    public IActivityService ActivityService =>
        _activity ??= new ActivityService(Uow, new ActivityMapper(_mapper));
}