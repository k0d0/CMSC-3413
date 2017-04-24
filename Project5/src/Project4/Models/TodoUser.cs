using Microsoft.AspNet.Identity.EntityFramework;

namespace Project5.Models
{
    public class TodoUser : IdentityUser
    {
        public string Password { get; set; }
    }
}
