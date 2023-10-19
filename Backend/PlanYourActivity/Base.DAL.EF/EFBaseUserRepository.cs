using Base.Conteracts;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class EFBaseUserRepository<TDomainEntity, TDalEntity, TDbContext> : EFBaseRepository<TDomainEntity, TDalEntity, Guid,
        TDbContext>
    where TDomainEntity : class, IDomainEntityId<Guid>
    where TDalEntity : class, IDomainEntityId
    where TDbContext : DbContext
{
    public EFBaseUserRepository(
        TDbContext dataContext,
        IMapper<TDomainEntity, TDalEntity> mapper
        ) : base(dataContext, mapper)
    {
    }
}

public class EFBaseUserRepository<TDomainEntity, TDalEntity, TKey, TDbContext>
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
    where TDbContext : DbContext
{
    protected TDbContext RepositoryDbContext;
    protected DbSet<TDomainEntity> RepositoryDbSet;
    protected readonly IMapper<TDomainEntity, TDalEntity> Mapper;

    public EFBaseUserRepository(TDbContext dataContext, IMapper<TDomainEntity, TDalEntity> mapper)
    {
        RepositoryDbContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        RepositoryDbSet = RepositoryDbContext.Set<TDomainEntity>();
        Mapper = mapper;
    }
}
