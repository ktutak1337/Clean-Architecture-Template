namespace CleanArchitectureTemplate.Application.Commands.WriteModels
{
    public class AddressWriteModel
    {
        public string City { get; }
        public string Street { get; }
        public string Province { get; }
        public string Country { get; }
        public string ZipCode { get; }

        public AddressWriteModel(string city, string street, string province, string country, string zipcode)
        {
            Street = street;
            City = city;
            Province = province;
            Country = country;
            ZipCode = zipcode;
        }
    }
}
