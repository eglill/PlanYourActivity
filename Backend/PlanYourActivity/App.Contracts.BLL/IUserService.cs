using App.BLL.DTO.Identity;
using App.Contracts.DAL;
using Base.Contracts.DAL;

namespace App.Contracts.BLL;

public interface IUserService : IBaseRepository<AppUser>, IUserRepositoryCustom<AppUser>
{
    // add your custom service methods here
}