namespace Domain.Interface.Repository;

public interface IRepository<TEntity, in TId> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity?> GetEntityById(TId id);
    Task CreateAsync(TEntity post);
    Task UpdateAsync(TEntity post);
    Task DeleteAsync(TEntity post);
    
}