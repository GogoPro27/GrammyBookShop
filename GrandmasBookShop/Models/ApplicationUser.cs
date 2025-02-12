using Microsoft.AspNetCore.Identity;

namespace GrandmasBookShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        // New property for the user's full name
        public string Name { get; set; }
    }
}
