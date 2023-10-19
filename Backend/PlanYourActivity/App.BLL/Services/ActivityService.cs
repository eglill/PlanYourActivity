using App.BLL.DTO;
using App.Contracts.BLL;
using App.Contracts.DAL;
using Base.BLL;
using Base.Conteracts;

namespace App.BLL.Services;

public class ActivityService : BaseEntityService<DAL.DTO.Activity, Activity, IActivityRepository>, IActivityService
{
    protected IAppUOW Uow;
    
    public ActivityService(IAppUOW uow, IMapper<DAL.DTO.Activity, Activity> mapper)
        : base(uow.ActivityRepository, mapper)
    {
        Uow = uow;
    }
}