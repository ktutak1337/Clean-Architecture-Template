namespace CleanArchitectureTemplate.Shared.Infrastructure.Persistence.Types
{
    public interface IIdentifiable<out TEntityId>
    {
        TEntityId Id { get; }
    }
}
