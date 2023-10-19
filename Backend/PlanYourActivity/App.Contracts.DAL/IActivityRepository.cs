using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IActivityRepository : IBaseRepository<Activity>, IActivityRepositoryCustom<Activity>
{
    // add here custom methods for repo only
}

public interface IActivityRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}