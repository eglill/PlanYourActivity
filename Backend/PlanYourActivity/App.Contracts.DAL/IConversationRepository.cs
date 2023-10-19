using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IConversationRepository : IBaseRepository<Conversation>, IConversationRepositoryCustom<Conversation>
{
    // add here custom methods for repo only
    void DeleteGroupConversation(Guid groupId);
    Task<Guid?> FindByGroupId(Guid groupId);
}

public interface IConversationRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}