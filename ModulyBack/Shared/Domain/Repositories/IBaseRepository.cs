namespace ModulyBack.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity>
{
    Task AddAsync(TEntity entity);

    Task<TEntity?> FindByIdAsync(Guid id);

    Task UpdateAsync(TEntity entity);
    Task AddSync(TEntity entity);


    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task<IEnumerable<TEntity>> ListAsync();
}