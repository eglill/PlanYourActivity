using App.BLL.DTO.Identity;
using App.BLL.Mappers;
using App.Contracts.BLL;
using App.Contracts.DAL;
using Base.BLL;

namespace App.BLL.Services;

public class UserService : BaseEntityService<DAL.DTO.identity.AppUser, AppUser, IUserRepository>, IUserService
{
    protected IAppUOW Uow;
    private AppUserMapper _mapper;

    public UserService(IAppUOW uow, AppUserMapper mapper)
        : base(uow.UserRepository, mapper)
    {
        Uow = uow;
        _mapper = mapper;
    }
}