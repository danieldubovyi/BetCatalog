using BetCatalog.Models;
using BetCatalog.Models.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BetCatalog.Infrastructure.EFCore.Repositories
{
    internal class EntityRepository<T>(BetCatalogDbContext dbContext)
        : IEntityRepository<T> where T : class, IEntity
    {
        protected readonly BetCatalogDbContext dbContext = dbContext;

        public async Task<T> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await dbContext.Set<T>().FindAsync([id], cancellationToken)
                ?? throw new ApplicationException("Entity not found");
        }

        public async Task<TProjection> GetAsync<TProjection>(int id, Expression<Func<T, TProjection>> projection, CancellationToken cancellationToken)
        {
            return await dbContext.Set<T>()
                .Where(e => e.Id == id)
                .Select(projection)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new ApplicationException("Entity not found");
        }

        public async Task<IReadOnlyCollection<T>> GetListAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Set<T>()
                .ToArrayAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<T>> GetListAsync(Expression<Func<T, bool>>? filter, CancellationToken cancellationToken)
        {
            if (filter == null)
            {
                return await GetListAsync(cancellationToken);
            }
            return await dbContext.Set<T>()
                .Where(filter)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<TProjection>> GetProjectedListAsync<TProjection>(
            Expression<Func<T, TProjection>> projection,
            CancellationToken cancellationToken)
        {
            return await dbContext.Set<T>()
                .Select(projection)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<TProjection>> GetProjectedListAsync<TProjection>(
            Expression<Func<T, bool>>? filter,
            Expression<Func<T, TProjection>> projection,
            CancellationToken cancellationToken)
        {
            if (filter == null)
            {
                return await GetProjectedListAsync(projection, cancellationToken);
            }
            return await dbContext.Set<T>()
                .Where(filter)
                .Select(projection)
                .ToArrayAsync(cancellationToken);
        }

        public async Task CreateAsync(T entity, CancellationToken cancellationToken)
        {
            dbContext.Set<T>().Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            dbContext.Set<T>().Attach(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(IReadOnlyCollection<T> entities, CancellationToken cancellationToken)
        {
            dbContext.Set<T>().AttachRange(entities);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var set = dbContext.Set<T>();
            var entity = await set.FindAsync([id], cancellationToken)
                ?? throw new ApplicationException("Entity not found");
            set.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task MergeAsync(
            IReadOnlyCollection<T> entitiesToCreate,
            IReadOnlyCollection<T> entitiesToUpdate,
            IReadOnlyCollection<T> entitiesToDelete,
            CancellationToken cancellationToken)
        {
            var set = dbContext.Set<T>();
            if (entitiesToCreate.Count > 0)
            {
                set.AddRange(entitiesToCreate);
            }

            if (entitiesToUpdate.Count > 0)
            {
                set.AttachRange(entitiesToUpdate);
            }

            if (entitiesToDelete.Count > 0)
            {
                set.RemoveRange(entitiesToDelete);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
