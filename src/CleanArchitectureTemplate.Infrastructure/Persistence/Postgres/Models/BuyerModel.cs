using System;
#if (shared && postgres)
using CleanArchitectureTemplate.Shared.Infrastructure.Persistence.Types;
#else
using CleanArchitectureTemplate.Infrastructure.Persistence.Types;
#endif

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models
{
    public class BuyerModel : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
        public DateTime CreatedAt { get; set; }        
    }
}
