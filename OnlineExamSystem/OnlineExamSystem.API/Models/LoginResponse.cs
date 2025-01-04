namespace OnlineExamSystem.API.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}