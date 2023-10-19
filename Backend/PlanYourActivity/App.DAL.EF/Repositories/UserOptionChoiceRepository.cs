using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserOptionChoiceRepository : EFBaseRepository<Domain.UserOptionChoice, UserOptionChoice, ApplicationDbContext>, IUserOptionChoiceRepository
{
    public UserOptionChoiceRepository(ApplicationDbContext dataContext, IMapper<Domain.UserOptionChoice, UserOptionChoice> mapper) : base(dataContext, mapper)
    {
    }

    public void DeleteAllUserChoicesInGroup(Guid groupId)
    {
        RepositoryDbSet.RemoveRange(RepositoryDbSet.Where(c => c.UserInGroup!.GroupId == groupId));
    }
}