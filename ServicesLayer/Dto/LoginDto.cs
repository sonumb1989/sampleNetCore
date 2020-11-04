using DataLayer.Entities;

namespace ServicesLayer.Dto
{
    public class LoginDto
    {
        public string Password { get; set; } // Password (length: 200)
        public string UserName { get; set; } // Password (length: 200)
    }
}
