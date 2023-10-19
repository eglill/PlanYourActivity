using App.BLL.DTO;
using App.Contracts.DAL;
using Base.Contracts.DAL;

namespace App.Contracts.BLL;

public interface ICountryService : IBaseRepository<Country>, ICountryRepositoryCustom<Country>
{
    // add your custom service methods here
}