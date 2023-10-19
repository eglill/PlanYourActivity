using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IFavoriteUserRepository : IBaseRepository<FavoriteUser>, IFavoriteUserRepositoryCustom<FavoriteUser>
{
    // add here custom methods for repo only
}

public interface IFavoriteUserRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}