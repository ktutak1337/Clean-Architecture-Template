namespace CleanArchitectureTemplate.Core.BuildingBlocks
{
    public interface IEntity<TEntityId> 
        where TEntityId: TypedIdValueBase
    {
         TEntityId Id { get; }
    }
}
