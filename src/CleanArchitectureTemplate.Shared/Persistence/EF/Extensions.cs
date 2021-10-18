using CleanArchitectureTemplate.Shared.Persistence.EF.Repositories;
using CleanArchitectureTemplate.Shared.Persistence.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Shared.Persistence.EF
{
    public static class Extensions
    {
        public static IServiceCollection AddEntityFrameworkRepository<TEntity, TIdentifiable, TDatabseContext>(this IServiceCollection services)
            where TEntity : class, IIdentifiable<TIdentifiable>
            where TDatabseContext : DbContext
                => services.AddTransient<IEntityFrameworkRepository<TEntity, TIdentifiable, TDatabseContext>, EntityFrameworkRepository<TEntity, TIdentifiable, TDatabseContext>>();
    }
}
