namespace CleanArchitectureTemplate.Shared.Kernel.BuildingBlocks
{
    public interface IEntity<out TEntityId>
        where TEntityId: TypedIdValueBase
    {
         TEntityId Id { get; }
    }
}
