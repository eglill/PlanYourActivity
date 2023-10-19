using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IPollOptionRepository : IBaseRepository<PollOption>, IPollOptionRepositoryCustom<PollOption>
{
    // add here custom methods for repo only
    void DeleteAllPollOptionsInGroup(Guid groupId);
}

public interface IPollOptionRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}