namespace OnlineExamSystem.API.Models
{
    public class LoginResponse : RegisterResponse
    {
        public string Token { get; set; } = null!;
    }
}