﻿using Microsoft.AspNetCore.Identity;

namespace MoviePortal.ApplicationCore.Model
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
