using Base.Conteracts;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace Base.BLL;

public class
    BaseEntityService<TDalEntity, TBllEntity, TRepository> :
        BaseEntityService<TDalEntity, TBllEntity, TRepository, Guid>, IEntityService<TBllEntity>
    where TDalEntity : class, IDomainEntityId
    where TBllEntity : class, IDomainEntityId
    where TRepository : IBaseRepository<TDalEntity>
{
    public BaseEntityService(TRepository repository, IMapper<TDalEntity, TBllEntity> mapper) : base(repository, mapper)
    {
    }
}

public class BaseEntityService<TDalEntity, TBllEntity, TRepository, TKey> : IEntityService<TBllEntity, TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TRepository : IBaseRepository<TDalEntity, TKey>
    where TKey : struct, IEquatable<TKey>
{
    protected readonly TRepository Repository;
    protected readonly IMapper<TDalEntity, TBllEntity> Mapper;

    public BaseEntityService(TRepository repository, IMapper<TDalEntity, TBllEntity> mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    public async Task<IEnumerable<TBllEntity>> AllAsync()
    {
        return (await Repository.AllAsync()).Select(e => Mapper.Map(e)!);
    }

    public async Task<TBllEntity?> FindAsync(TKey id)
    {
        return Mapper.Map(await Repository.FindAsync(id));
    }

    public TBllEntity Add(TBllEntity entity)
    {
        return Mapper.Map(Repository.Add(Mapper.Map(entity)!))!;
    }

    public TBllEntity Update(TBllEntity entity)
    {
        return Mapper.Map(Repository.Update(Mapper.Map(entity)!))!;
    }

    public TBllEntity Remove(TBllEntity entity)
    {
        return Mapper.Map(Repository.Remove(Mapper.Map(entity)!))!;
    }

    public async Task<TBllEntity?> RemoveAsync(TKey id)
    {
        return Mapper.Map(await Repository.RemoveAsync(id));
    }
}