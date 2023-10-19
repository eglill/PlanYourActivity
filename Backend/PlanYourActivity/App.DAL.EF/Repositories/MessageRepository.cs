using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class MessageRepository : EFBaseRepository<Domain.Message, Message, ApplicationDbContext>, IMessageRepository
{
    public MessageRepository(ApplicationDbContext dataContext, IMapper<Domain.Message, Message> mapper) : base(dataContext, mapper)
    {
    }

    public void DeleteAllMessagesInGroup(Guid groupId)
    {
        RepositoryDbSet.RemoveRange(RepositoryDbSet.Where(c => c.Conversation!.GroupId == groupId));
    }
}