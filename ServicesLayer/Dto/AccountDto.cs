using DataLayer.Entities;

namespace ServicesLayer.Dto
{
    public class AccountDto : BaseDto
    {
        public string FirstName { get; set; } // FirstName (length: 50)
        public string LastName { get; set; } // LastName (length: 100)
        public string LoginName { get; set; } // LoginName (length: 100)
        public string Email { get; set; } // Email (length: 200)
        public string Address { get; set; } // Address (length: 300)
        public string Ward { get; set; } // Ward (length: 50)
        public string District { get; set; } // District (length: 50)
        public string City { get; set; } // City (length: 50)
        public string PhoneNumber { get; set; } // PhoneNumber (length: 50)
        public string Password { get; set; } // Password (length: 200)
        public string UserName { get; set; } // Password (length: 200)
    }
}
