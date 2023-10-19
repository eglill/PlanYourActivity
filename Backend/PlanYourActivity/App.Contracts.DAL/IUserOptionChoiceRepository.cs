using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserOptionChoiceRepository : IBaseRepository<UserOptionChoice>, IUserOptionChoiceRepositoryCustom<UserOptionChoice>
{
    // add here custom methods for repo only
    void DeleteAllUserChoicesInGroup(Guid groupId);
}

public interface IUserOptionChoiceRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
}