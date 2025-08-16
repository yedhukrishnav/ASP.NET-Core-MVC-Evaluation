using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Evaluation.Models
{
    public class SalaryViewModel
    {
        [Key]
        public int user_id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime dob { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Account Type is required")]
        public string account_type { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
        public string? photo_name { get; set; }
        public string? photo_path { get; set; }
        [Key]
        public int salary_id { get; set; }

        [Required]
        public string month { get; set; }
        [Required]
        public int year { get; set; }
        [Required]
        public int salary { get; set; }

    }
}
