using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ActivityRepository : EFBaseRepository<Domain.Activity, Activity, ApplicationDbContext>, IActivityRepository
{
    public ActivityRepository(ApplicationDbContext dataContext, IMapper<Domain.Activity, Activity> mapper) : base(dataContext, mapper)
    {
    }
}