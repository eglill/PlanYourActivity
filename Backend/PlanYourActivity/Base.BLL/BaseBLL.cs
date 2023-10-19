using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace Base.BLL;

public abstract class BaseBLL<TUOW> : IBaseBLL
    where TUOW : IBaseUOW
{
    public abstract Task<int> SaveChangesAsync();
}