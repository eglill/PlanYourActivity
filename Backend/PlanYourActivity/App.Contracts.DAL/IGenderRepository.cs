using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IGenderRepository : IBaseRepository<Gender>, IGenderRepositoryCustom<Gender>
{
    // add here custom methods for repo only
}

public interface IGenderRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}