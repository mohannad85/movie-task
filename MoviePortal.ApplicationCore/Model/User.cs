using Microsoft.AspNetCore.Identity;

namespace MoviePortal.ApplicationCore.Model
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
