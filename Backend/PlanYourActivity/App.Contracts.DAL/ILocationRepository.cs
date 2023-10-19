using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ILocationRepository : IBaseRepository<Location>, ILocationRepositoryCustom<Location>
{
    // add here custom methods for repo only
}

public interface ILocationRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}