using System.ComponentModel.DataAnnotations;

namespace nw_api.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Email Required!")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password Required!")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Confirm Password Required!")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "First Name Required!")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last Name Required!")]
        public string LastName { get; set; }
    }
}