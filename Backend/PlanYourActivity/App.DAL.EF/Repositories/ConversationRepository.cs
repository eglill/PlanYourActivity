using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ConversationRepository : EFBaseRepository<Domain.Conversation, Conversation, ApplicationDbContext>, IConversationRepository
{
    public ConversationRepository(ApplicationDbContext dataContext, IMapper<Domain.Conversation, Conversation> mapper) : base(dataContext, mapper)
    {
    }

    public void DeleteGroupConversation(Guid groupId)
    {
        RepositoryDbSet.RemoveRange(RepositoryDbSet.Where(c => c.GroupId == groupId));
    }

    public async Task<Guid?> FindByGroupId(Guid groupId)
    {
        return (await RepositoryDbSet.Where(c => c.GroupId == groupId).FirstOrDefaultAsync())?.Id;
    }

    public override async Task<Conversation?> FindAsync(Guid groupId)
    {
        var conversation = await RepositoryDbSet
            .Include(c => c.Messages!.OrderBy(a => a.CreatedAt))
                .ThenInclude(c => c.Creator)
            .Where(c => c.GroupId == groupId)
            .FirstOrDefaultAsync();
        
        return Mapper.Map(conversation);
    }
}