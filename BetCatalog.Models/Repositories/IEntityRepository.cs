using System.Linq.Expressions;

namespace BetCatalog.Models.Interfaces.Repositories
{
    public interface IEntityRepository<T> where T : IEntity
    {
        Task<IReadOnlyCollection<T>> GetListAsync(CancellationToken cancellationToken);

        Task<IReadOnlyCollection<T>> GetListAsync(Expression<Func<T, bool>>? filter, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<TProjection>> GetProjectedListAsync<TProjection>(
            Expression<Func<T, TProjection>> projection,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<TProjection>> GetProjectedListAsync<TProjection>(
            Expression<Func<T, bool>>? filter,
            Expression<Func<T, TProjection>> projection,
            CancellationToken cancellationToken);

        Task<T> GetAsync(int id, CancellationToken cancellationToken);

        Task<TProjection> GetAsync<TProjection>(int id, Expression<Func<T, TProjection>> projection, CancellationToken cancellationToken);

        Task CreateAsync(T entity, CancellationToken cancellationToken);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        Task UpdateAsync(IReadOnlyCollection<T> entities, CancellationToken cancellationToken);

        Task DeleteAsync(int id, CancellationToken cancellationToken);

        Task MergeAsync(
            IReadOnlyCollection<T> entitiesToCreate,
            IReadOnlyCollection<T> entitiesToUpdate,
            IReadOnlyCollection<T> entitiesToDelete,
            CancellationToken cancellationToken);
    }
}
