using App.BLL.DTO;
using App.Contracts.BLL;
using App.Contracts.DAL;
using Base.BLL;
using Base.Conteracts;

namespace App.BLL.Services;

public class CountryService : BaseEntityService<DAL.DTO.Country, Country, ICountryRepository>, ICountryService
{
    protected IAppUOW Uow;
    
    public CountryService(IAppUOW uow, IMapper<DAL.DTO.Country, Country> mapper)
        : base(uow.CountryRepository, mapper)
    {
        Uow = uow;
    }
}