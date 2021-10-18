namespace CleanArchitectureTemplate.Shared.Kernel.BuildingBlocks
{
    public interface IBusinessRule
    {
        string Code { get; }
        string Message { get; }
        bool IsBroken();
    }
}
