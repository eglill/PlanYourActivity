using App.BLL.DTO;
using App.Contracts.DAL;
using Base.Contracts.DAL;

namespace App.Contracts.BLL;

public interface IConversationService : IBaseRepository<Conversation>, IConversationRepositoryCustom<Conversation>
{
    // add your custom service methods here
    Task<Conversation?> FindAsync(Guid groupId, Guid userId);
    Task<bool?> AddMessage(AddMessage addMessage, Guid userId);

}