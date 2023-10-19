using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IMessageRepository : IBaseRepository<Message>, IMessageRepositoryCustom<Message>
{
    // add here custom methods for repo only
    void DeleteAllMessagesInGroup(Guid groupId);
}

public interface IMessageRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}