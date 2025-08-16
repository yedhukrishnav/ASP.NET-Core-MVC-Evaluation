using System.ComponentModel.DataAnnotations;

namespace Evaluation.Models
{
    public class Salary
    {
        [Key]
        public int salary_id { get; set; }
        [Required]
        public int user_id { get; set; }
        [Required]
        public string month { get; set; }
        [Required]
        public int year { get; set; }
        [Required]
        public int salary { get; set; }
    }
}
