using CleanArchitectureTemplate.Core.BuildingBlocks;

namespace CleanArchitectureTemplate.Core.ValueObjects
{
    public class Address : ValueObject
    {
        public string City { get; private set; }
        public string Street { get; private set; }
        public string Province { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

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
