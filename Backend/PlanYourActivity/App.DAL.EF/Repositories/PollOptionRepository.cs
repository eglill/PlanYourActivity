using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class PollOptionRepository : EFBaseRepository<Domain.PollOption, PollOption, ApplicationDbContext>, IPollOptionRepository
{
    public PollOptionRepository(ApplicationDbContext dataContext, IMapper<Domain.PollOption, PollOption> mapper) : base(dataContext, mapper)
    {
    }

    public void DeleteAllPollOptionsInGroup(Guid groupId)
    {
        RepositoryDbSet.RemoveRange(RepositoryDbSet.Where(c => c.PollQuestion!.Creator!.GroupId == groupId));
    }
}