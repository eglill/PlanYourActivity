using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CountryRepository : EFBaseRepository<Domain.Country, Country, ApplicationDbContext>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext dataContext, IMapper<Domain.Country, Country> mapper) : base(dataContext, mapper)
    {
    }
}