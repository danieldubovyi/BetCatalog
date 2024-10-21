using Microsoft.AspNetCore.Identity;

namespace BetCatalog.Models.Users
{
    public class UserRole : IdentityRole
    {
        public const string Admin = "Admin";
    }
}
