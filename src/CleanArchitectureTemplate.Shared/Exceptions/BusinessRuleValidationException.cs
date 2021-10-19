using CleanArchitectureTemplate.Shared.Kernel.BuildingBlocks;

namespace CleanArchitectureTemplate.Shared.Exceptions
{
    public class BusinessRuleValidationException : DomainException
    {
        public IBusinessRule BusinessRule { get; }

        public BusinessRuleValidationException(IBusinessRule businessRule)
            : base(businessRule.Message)
                => BusinessRule = businessRule;
    }
}
