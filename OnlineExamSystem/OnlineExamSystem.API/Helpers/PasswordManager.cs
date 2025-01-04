using System.Security.Cryptography;
using System.Text;

namespace OnlineExamSystem.API.Helpers
{
    public class PasswordManager
    {
        public string HashPassword(string password)
        {
            using var sha256Hash = SHA256.Create();

            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();

            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
        
        public bool VerifyPassword(string password, string hashedPassword)
            => hashedPassword.Equals(HashPassword(password), StringComparison.InvariantCulture);
    }
}