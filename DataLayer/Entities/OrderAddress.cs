using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class OrderAddress : BaseEntity
    {
        public string FirstName { get; set; } // FirstName
        public string LastName { get; set; } // LastName
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string State { get; set; } // State
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string Phone { get; set; } // Phone
        public string Email { get; set; } // Email
        public bool IsBilling { get; set; } // IsBilling
        public bool IsShipping { get; set; } // IsShipping

    }
}
