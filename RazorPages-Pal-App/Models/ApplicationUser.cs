using Microsoft.AspNetCore.Identity;

namespace RazorPages_Pal_App.Models
{
    public class ApplicationUser :IdentityUser
    {
        public DateTime CreationTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool IsActived { get; set; }
    }
}
