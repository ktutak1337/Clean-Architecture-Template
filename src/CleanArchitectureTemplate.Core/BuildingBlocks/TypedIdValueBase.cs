using System;

namespace CleanArchitectureTemplate.Core.BuildingBlocks
{
    public class TypedIdValueBase : IEquatable<TypedIdValueBase>
    {
        public Guid Value { get; }

        protected TypedIdValueBase(Guid value)
        {
            if(value == null || value == Guid.Empty)
            {
                throw new InvalidOperationException();
            }

            Value = value;
        }
            
        public bool Equals(TypedIdValueBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TypedIdValueBase) obj);
        }

        public override int GetHashCode()
            => Value.GetHashCode();

        public static bool operator ==(TypedIdValueBase obj1, TypedIdValueBase obj2)
        {
            if (object.Equals(obj1, null))
            {
                if (object.Equals(obj2, null))
                {
                    return true;
                }
                return false;
            }
            return obj1.Equals(obj2);
        }

        public static bool operator !=(TypedIdValueBase a, TypedIdValueBase b)
            => !(a == b);

        public static implicit operator Guid(TypedIdValueBase typedId)
            => typedId.Value;

        public override string ToString() => Value.ToString();
    }
}
