using App.BLL.DTO;
using App.Contracts.DAL;
using Base.Contracts.DAL;

namespace App.Contracts.BLL;

public interface IColourService : IBaseRepository<Colour>, IColourRepositoryCustom<Colour>
{
    // add your custom service methods here
}