using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICountryRepository : IBaseRepository<Country>, ICountryRepositoryCustom<Country>
{
    // add here custom methods for repo only
}

public interface ICountryRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}