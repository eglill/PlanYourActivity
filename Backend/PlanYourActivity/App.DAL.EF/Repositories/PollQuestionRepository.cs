using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class PollQuestionRepository : EFBaseRepository<Domain.PollQuestion, PollQuestion, ApplicationDbContext>, IPollQuestionRepository
{
    public PollQuestionRepository(ApplicationDbContext dataContext, IMapper<Domain.PollQuestion, PollQuestion> mapper) : base(dataContext, mapper)
    {
    }

    public void DeleteAllPollQuestionsInGroup(Guid groupId)
    {
        RepositoryDbSet.RemoveRange(RepositoryDbSet.Where(c => c.Creator!.GroupId == groupId));
    }
}