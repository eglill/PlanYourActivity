using App.Contracts.DAL;
using App.DAL.DTO;
using App.DAL.DTO.identity;
using Base.Conteracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
namespace App.DAL.EF.Repositories;

public class UserRepository : EFBaseUserRepository<Domain.Identity.AppUser, AppUser, ApplicationDbContext>, IUserRepository
{
    public UserRepository(ApplicationDbContext dataContext, IMapper<Domain.Identity.AppUser, AppUser> mapper) : base(dataContext, mapper)
    {
    }

    public async Task<AppUser?> GetUser(Guid userId)
    {
        var user = Mapper.Map(await RepositoryDbSet
            .Where(c => c.Id == userId)
            .AsNoTracking()
            .FirstOrDefaultAsync());
        
        return user;
    }
}