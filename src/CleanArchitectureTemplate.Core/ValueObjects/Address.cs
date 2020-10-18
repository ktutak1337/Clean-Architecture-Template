#if (shared)
using CleanArchitectureTemplate.Shared.BuildingBlocks;
#else
using CleanArchitectureTemplate.Core.BuildingBlocks;
#endif

namespace CleanArchitectureTemplate.Core.ValueObjects
{
    public class Address : ValueObject
    {
        public string City { get; }
        public string Street { get; }
        public string Province { get; }
        public string Country { get; }
        public string ZipCode { get; }

        private Address() { }

        public Address(string city, string street, string province, string country, string zipcode)
        {
            Street = street;
            City = city;
            Province = province;
            Country = country;
            ZipCode = zipcode;
        }
    }
}
