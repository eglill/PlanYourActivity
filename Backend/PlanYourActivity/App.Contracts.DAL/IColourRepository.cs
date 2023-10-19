using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IColourRepository : IBaseRepository<Colour>, IColourRepositoryCustom<Colour>
{
    // add here custom methods for repo only
}

public interface IColourRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}