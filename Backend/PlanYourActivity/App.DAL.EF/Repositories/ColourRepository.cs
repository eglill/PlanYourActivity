using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Conteracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ColourRepository : EFBaseRepository<Domain.Colour, Colour, ApplicationDbContext>, IColourRepository
{
    public ColourRepository(ApplicationDbContext dataContext, IMapper<Domain.Colour, Colour> mapper) : base(dataContext, mapper)
    {
    }
}