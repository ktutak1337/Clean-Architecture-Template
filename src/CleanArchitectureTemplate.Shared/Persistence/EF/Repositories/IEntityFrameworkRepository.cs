using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Shared.Persistence.Types;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Shared.Persistence.EF.Repositories
{
    public interface IEntityFrameworkRepository<TEntity, in TIdentifiable, TDatabseContext>
        where TEntity : class, IIdentifiable<TIdentifiable>
        where TDatabseContext : DbContext
    {
		Task<TEntity> GetAsync(TIdentifiable id, params Expression<Func<TEntity, object>>[] includes);
		Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task DeleteAsync(TIdentifiable id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
