using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CleanArchitectureTemplate.Shared.Exceptions;

namespace CleanArchitectureTemplate.Shared.Kernel.BuildingBlocks
{
    public abstract class ValueObject
    {
        private List<PropertyInfo> _properties;
        private List<FieldInfo> _fields;

        public static bool operator ==(ValueObject obj1, ValueObject obj2)
        {
            if (Equals(obj1, null))
            {
                if (Equals(obj2, null))
                {
                    return true;
                }
                return false;
            }
            return obj1.Equals(obj2);
        }

        public static bool operator !=(ValueObject obj1, ValueObject obj2)
            => !(obj1 == obj2);

        public bool Equals(ValueObject obj)
            => Equals(obj as object);

        public override bool Equals(object obj)
            => obj is null || GetType() != obj.GetType()
                ? false
                : GetProperties().All(p => PropertiesAreEqual(obj, p))
                    && GetFields().All(f => FieldsAreEqual(obj, f));

        private bool PropertiesAreEqual(object obj, PropertyInfo p)
            => Equals(p.GetValue(this, null), p.GetValue(obj, null));

        private bool FieldsAreEqual(object obj, FieldInfo f)
            => Equals(f.GetValue(this), f.GetValue(obj));

        private IEnumerable<PropertyInfo> GetProperties()
        {
            if (_properties is null)
            {
                _properties = GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToList();
            }

            return _properties;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            if (_fields is null)
            {
                _fields = GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .ToList();
            }

            return _fields;
        }

        public override int GetHashCode()
        {
            unchecked   //allow overflow
            {
                int hash = 17;
                foreach (var property in GetProperties())
                {
                    var value = property.GetValue(this, null);
                    hash = HashValue(hash, value);
                }

                foreach (var field in GetFields())
                {
                    var value = field.GetValue(this);
                    hash = HashValue(hash, value);
                }

                return hash;
            }
        }

        private int HashValue(int seed, object value)
        {
            var currentHash = value?.GetHashCode() ?? 0;
            return seed * 23 + currentHash;
        }

        protected static void CheckRule(IBusinessRule businessRule)
        {
            if (businessRule.IsBroken())
            {
                throw new BusinessRuleValidationException(businessRule);
            }
        }
    }
}
