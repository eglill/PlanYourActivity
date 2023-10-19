using App.BLL.DTO;
using App.Contracts.BLL;
using App.Contracts.DAL;
using Base.BLL;
using Base.Conteracts;

namespace App.BLL.Services;

public class ColourService : BaseEntityService<DAL.DTO.Colour, Colour, IColourRepository>, IColourService
{
    protected IAppUOW Uow;
    
    public ColourService(IAppUOW uow, IMapper<DAL.DTO.Colour, Colour> mapper)
        : base(uow.ColourRepository, mapper)
    {
        Uow = uow;
    }
}