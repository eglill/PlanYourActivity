using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IPollQuestionRepository : IBaseRepository<PollQuestion>, IPollQuestionRepositoryCustom<PollQuestion>
{
    // add here custom methods for repo only
    void DeleteAllPollQuestionsInGroup(Guid groupId);
}

public interface IPollQuestionRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}