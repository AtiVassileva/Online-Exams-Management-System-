using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.API.Models
{
    public class RegisterRequest
    {
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [MaxLength(50)]
        public string Password { get; set; } = null!;

        [MaxLength(50)]
        public string ConfirmPassword { get; set; } = null!;

        [AllowedValues("Student, Teacher", ErrorMessage = "Invalid role selection!")]
        public string Role { get; set; } = null!;
    }
}