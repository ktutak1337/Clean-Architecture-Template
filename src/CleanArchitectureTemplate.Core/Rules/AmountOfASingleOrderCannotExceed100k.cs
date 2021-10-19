#if (shared)
using CleanArchitectureTemplate.Shared.Kernel.BuildingBlocks;
#else
using CleanArchitectureTemplate.Core.BuildingBlocks;
#endif

namespace CleanArchitectureTemplate.Core.Rules
{
    public class AmountOfASingleOrderCannotExceed100k : IBusinessRule
    {
        private readonly decimal _totalPrice;
        public string Message => "The amount of a single order cannot exceed $100 000.";

        public AmountOfASingleOrderCannotExceed100k(decimal totalPrice)
            => _totalPrice = totalPrice;

        public bool IsBroken()
            => _totalPrice <= 100000.00m ? false : true;
    }
}
