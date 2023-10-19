using App.BLL.DTO;
using App.Contracts.BLL;
using App.Contracts.DAL;
using Base.BLL;
using Base.Conteracts;

namespace App.BLL.Services;

public class GenderService : BaseEntityService<DAL.DTO.Gender, Gender, IGenderRepository>, IGenderService
{
    protected IAppUOW Uow;
    
    public GenderService(IAppUOW uow, IMapper<DAL.DTO.Gender, Gender> mapper)
        : base(uow.GenderRepository, mapper)
    {
        Uow = uow;
    }
}