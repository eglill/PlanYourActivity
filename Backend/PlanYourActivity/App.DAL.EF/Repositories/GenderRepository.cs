using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class GenderRepository : EFBaseRepository<Domain.Gender, Gender, ApplicationDbContext>, IGenderRepository
{
    public GenderRepository(ApplicationDbContext dataContext, IMapper<Domain.Gender, Gender> mapper) : base(dataContext, mapper)
    {
    }
}