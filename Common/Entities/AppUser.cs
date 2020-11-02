﻿

using Microsoft.AspNetCore.Identity;

namespace Common.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
