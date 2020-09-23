namespace CleanArchitectureTemplate.Infrastructure.Persistence.Types
{
    public interface IIdentifiable<out TEntityId>
    {
        TEntityId Id { get; }
    }
}
