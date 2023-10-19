using App.BLL.DTO;
using App.Contracts.DAL;
using Base.Contracts.DAL;

namespace App.Contracts.BLL;

public interface IActivityService : IBaseRepository<Activity>, IActivityRepositoryCustom<Activity>
{
    // add your custom service methods here
}