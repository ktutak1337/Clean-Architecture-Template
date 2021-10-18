namespace CleanArchitectureTemplate.Shared.Persistence.Types
{
    public interface IIdentifiable<out TEntityId>
    {
        TEntityId Id { get; }
    }
}
