using App.DAL.DTO.identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserRepository : IBaseRepository<AppUser>, IUserRepositoryCustom<AppUser>
{
    // add here custom methods for repo only
    Task<AppUser?> GetUser(Guid userId);

}

public interface IUserRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}