namespace CleanArchitectureTemplate.Core.BuildingBlocks
{
    public interface IBusinessRule
    {
        string Code { get; }
        string Message { get; }
        bool IsBroken();
    }
}
