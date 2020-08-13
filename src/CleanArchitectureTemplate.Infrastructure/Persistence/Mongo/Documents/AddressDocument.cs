namespace CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents
{
    public class AddressDocument
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
