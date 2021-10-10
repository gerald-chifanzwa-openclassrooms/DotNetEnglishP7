using Microsoft.AspNetCore.Identity;

namespace WebApi.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FullName { get; set; }
    }
}
