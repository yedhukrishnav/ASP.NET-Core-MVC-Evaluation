using System.ComponentModel.DataAnnotations;

namespace Evaluation.Models
{
    public class LoginViewModels
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password must be at least 3 characters long", MinimumLength = 3)]
        public string password { get; set; }
    }
}
