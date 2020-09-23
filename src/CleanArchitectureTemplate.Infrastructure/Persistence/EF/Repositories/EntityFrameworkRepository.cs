using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Infrastructure.Persistence.Types;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EF.Repositories
{
    public class EntityFrameworkRepository<TEntity, TIdentifiable, TDatabseContext> : IEntityFrameworkRepository<TEntity, TIdentifiable, TDatabseContext>
        where TEntity : class, IIdentifiable<TIdentifiable>
        where TDatabseContext : DbContext
    {
        private readonly TDatabseContext _databseContext;
        private readonly DbSet<TEntity> _databaseSet;
        
        public EntityFrameworkRepository(TDatabseContext databseContext)
        {
            _databseContext = databseContext ?? throw new ArgumentNullException(nameof(databseContext));
            _databaseSet = _databseContext.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(TIdentifiable id) 
            => await _databaseSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await _databaseSet.SingleOrDefaultAsync(predicate);

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await _databaseSet.Where(predicate).ToListAsync();

        public async Task AddAsync(TEntity entity)
        {
            await _databaseSet.AddAsync(entity);
            await _databseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _databaseSet.Update(entity);
            await _databseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TIdentifiable id)
        {
            var entity = await GetAsync(id);
            _databaseSet.Remove(entity);
            await _databseContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await _databaseSet.AnyAsync(predicate);
    }
}
