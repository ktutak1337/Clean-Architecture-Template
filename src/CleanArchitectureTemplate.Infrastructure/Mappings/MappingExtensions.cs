using AutoMapper;

namespace CleanArchitectureTemplate.Infrastructure.Mappings
{
    public static class MappingExtensions
    {
        public static TEntityDestination MapSingle<TEntitySource, TEntityDestination>(this TEntitySource entity, IMapper mapper) 
            where TEntitySource : class
            where TEntityDestination: class
                => entity == null 
                    ? null
                    : mapper.Map<TEntitySource, TEntityDestination>(entity);
    }
}
