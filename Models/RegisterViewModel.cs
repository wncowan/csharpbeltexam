using System.ComponentModel.DataAnnotations;

namespace csharpbeltexam.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter a first name")]
        [MinLength(4, ErrorMessage = "First name must be at least 4 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter an Alias name")]
        [MinLength(4, ErrorMessage = "Alias name must be at least 4 characters")]
        public string Alias { get; set; }

        [Required(ErrorMessage = "You must enter an email")]
        [EmailAddress]
        [MinLength(3, ErrorMessage = "Email must be at least 3 characters")]
        [MaxLength(20, ErrorMessage = "Email cannot be more than 20 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter a password")]
        [MinLength(8, ErrorMessage = "Password cannot be less than 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}