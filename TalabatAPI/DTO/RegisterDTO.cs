
using System.ComponentModel.DataAnnotations;

namespace TalabatAPI.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*(),.?\":{}|<>])[a-zA-Z\\d!@#$%^&*(),.?\":{}|<>]{8,}$",
            ErrorMessage = "The password must be at least 8 characters long and include at least one uppercase letter, " +
            "one lowercase letter, one number, and one special character (e.g., !@#$%^&*)")]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
     
    }
}
