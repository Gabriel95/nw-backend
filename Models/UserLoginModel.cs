using System.ComponentModel.DataAnnotations;

namespace nw_api.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}