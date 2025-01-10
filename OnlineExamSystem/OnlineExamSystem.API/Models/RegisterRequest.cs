using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.API.Models
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string ConfirmPassword { get; set; } = null!;

        [Required] public string Role { get; set; } = null!;
    }
}