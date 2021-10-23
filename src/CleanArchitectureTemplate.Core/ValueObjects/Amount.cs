#if (shared)
using CleanArchitectureTemplate.Shared.Kernel.BuildingBlocks;
#else
using CleanArchitectureTemplate.Core.BuildingBlocks;
#endif
using CleanArchitectureTemplate.Core.Exceptions;

namespace CleanArchitectureTemplate.Core.ValueObjects
{
    public class Amount : ValueObject
    {
        public decimal Value { get; }

        public Amount(decimal value)
        {
            if (value is < 0 or > 1000000)
            {
                throw new InvalidAmountException(value);
            }

            Value = value;
        }

        public static implicit operator Amount(decimal value) => new(value);

        public static implicit operator decimal(Amount value) => value.Value;

        public static Amount operator +(Amount x, Amount y) => x.Value + y.Value;

        public static Amount operator -(Amount x, Amount y) => x.Value - y.Value;
    }
}
