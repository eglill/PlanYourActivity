using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class InviteToGroupRepository : EFBaseRepository<Domain.InviteToGroup, InviteToGroup, ApplicationDbContext>, IInviteToGroupRepository
{
    public InviteToGroupRepository(ApplicationDbContext dataContext, IMapper<Domain.InviteToGroup, InviteToGroup> mapper) : base(dataContext, mapper)
    {
    }

    public void DeleteAllInvitesToGroup(Guid groupId)
    {
        RepositoryDbSet.RemoveRange(RepositoryDbSet.Where(c => c.GroupId == groupId));
    }
}