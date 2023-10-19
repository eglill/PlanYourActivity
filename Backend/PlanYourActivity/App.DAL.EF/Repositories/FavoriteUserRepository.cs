using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class FavoriteUserRepository : EFBaseRepository<Domain.FavoriteUser, FavoriteUser, ApplicationDbContext>, IFavoriteUserRepository
{
    public FavoriteUserRepository(ApplicationDbContext dataContext, IMapper<Domain.FavoriteUser, FavoriteUser> mapper) : base(dataContext, mapper)
    {
    }
}