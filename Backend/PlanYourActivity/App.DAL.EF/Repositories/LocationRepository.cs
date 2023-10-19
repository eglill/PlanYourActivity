using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class LocationRepository : EFBaseRepository<Domain.Location, Location, ApplicationDbContext>, ILocationRepository
{
    public LocationRepository(ApplicationDbContext dataContext, IMapper<Domain.Location, Location> mapper) : base(dataContext, mapper)
    {
    }
}