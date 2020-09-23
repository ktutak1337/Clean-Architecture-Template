namespace CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models
{
    public class AddressModel
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
