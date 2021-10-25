namespace CleanArchitectureTemplate.Core.BuildingBlocks
{
    public interface IBusinessRule
    {
        string Message { get; }
        bool IsBroken();
    }
}
