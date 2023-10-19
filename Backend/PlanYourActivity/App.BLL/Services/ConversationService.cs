using App.BLL.DTO;
using App.BLL.Mappers;
using App.Contracts.BLL;
using App.Contracts.DAL;
using Base.BLL;
using Conversation = App.BLL.DTO.Conversation;

namespace App.BLL.Services;

public class ConversationService : BaseEntityService<DAL.DTO.Conversation, Conversation, IConversationRepository>, IConversationService
{
    protected IAppUOW Uow;
    private ConversationMapper _mapper;

    public ConversationService(IAppUOW uow, ConversationMapper mapper)
        : base(uow.ConversationRepository, mapper)
    {
        Uow = uow;
        _mapper = mapper;
    }

    public async Task<Conversation?> FindAsync(Guid groupId, Guid userId)
    {
        var isUserInGroup = await Uow.UserInGroupRepository.IsUserInGroup(groupId, userId);
        if (isUserInGroup)
        {
            var conversation = await Uow.ConversationRepository.FindAsync(groupId);
            return conversation == null ? new Conversation() : Mapper.Map(conversation);
        }

        return null;
    }

    public async Task<bool?> AddMessage(AddMessage addMessage, Guid userId)
    {
        var isUserInGroup = await Uow.UserInGroupRepository.IsUserInGroup(addMessage.groupId, userId);
        var conversationId = await Uow.ConversationRepository.FindByGroupId(addMessage.groupId);
        
        if (isUserInGroup)
        {
            if (conversationId == null)
            {
                return false;
            }
            var message = new App.DAL.DTO.Message {CreatorId = userId, ConversationId = (Guid) conversationId, CreatedAt = DateTime.UtcNow, Content = addMessage.Content};
            Uow.MessageRepository.Add(message);
            return true;
        }
        return null;
    }
}