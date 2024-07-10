using System.ComponentModel.DataAnnotations;

namespace RazorPages_Pal_App.Dto_s
{
    public class UserDto
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        [Required]
        public DateTime LastLoginTime { get; set; }

       
    }
}
