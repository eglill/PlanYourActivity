using Base.Conteracts;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class EFBaseRepository<TDomainEntity, TDalEntity, TDbContext> : EFBaseRepository<TDomainEntity, TDalEntity, Guid,
        TDbContext>,
    IBaseRepository<TDalEntity>
    where TDomainEntity : class, IDomainEntityId<Guid>
    where TDalEntity : class, IDomainEntityId
    where TDbContext : DbContext
{
    public EFBaseRepository(
        TDbContext dataContext,
        IMapper<TDomainEntity, TDalEntity> mapper
        ) : base(dataContext, mapper)
    {
    }
}

public class EFBaseRepository<TDomainEntity, TDalEntity, TKey, TDbContext> : IBaseRepository<TDalEntity, TKey>
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
    where TDbContext : DbContext
{
    protected TDbContext RepositoryDbContext;
    protected DbSet<TDomainEntity> RepositoryDbSet;
    protected readonly IMapper<TDomainEntity, TDalEntity> Mapper;

    public EFBaseRepository(TDbContext dataContext, IMapper<TDomainEntity, TDalEntity> mapper)
    {
        RepositoryDbContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        RepositoryDbSet = RepositoryDbContext.Set<TDomainEntity>();
        Mapper = mapper;
    }


    public virtual async Task<IEnumerable<TDalEntity>> AllAsync()
    {
        var data = await RepositoryDbSet
            .ToListAsync();
        return data.Select(x => Mapper.Map(x)!);
    }

    public virtual async Task<TDalEntity?> FindAsync(TKey id)
    {
        return Mapper.Map(await RepositoryDbSet.FindAsync(id));
    }

    public virtual TDalEntity Add(TDalEntity entity)
    {
        return Mapper.Map(RepositoryDbSet.Add(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity Update(TDalEntity entity)
    {
        return Mapper.Map(RepositoryDbSet.Update(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity Remove(TDalEntity entity)
    {
        return Mapper.Map(RepositoryDbSet.Remove(Mapper.Map(entity)!).Entity)!;
    }

    public virtual async Task<TDalEntity?> RemoveAsync(TKey id)
    {
        var entity = await FindAsync(id);
        return entity != null ? Remove(entity) : null;
    }
}
