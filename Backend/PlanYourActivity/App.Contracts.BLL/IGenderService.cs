using App.BLL.DTO;
using App.Contracts.DAL;
using Base.Contracts.DAL;

namespace App.Contracts.BLL;

public interface IGenderService : IBaseRepository<Gender>, IGenderRepositoryCustom<Gender>
{
    // add your custom service methods here
}