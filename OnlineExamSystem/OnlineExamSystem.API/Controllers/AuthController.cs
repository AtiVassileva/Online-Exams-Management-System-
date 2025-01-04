using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.API.Helpers;
using OnlineExamSystem.API.Models;
using OnlineExamSystem.Data;
using LoginRequest = OnlineExamSystem.API.Models.LoginRequest;

namespace OnlineExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly OnlineExamSystemContext _context;
        private readonly JwtTokenGenerator _jwtTokenHelper;
        private readonly PasswordManager _passwordManager;

        public AuthController(OnlineExamSystemContext context, IConfiguration configuration, PasswordManager passwordManager)
        {
            _context = context;
            _jwtTokenHelper = new JwtTokenGenerator(configuration);
            _passwordManager = passwordManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password!" });
            }

            var hashedPassword = user.PasswordHash;
            var isLoginPasswordValid = _passwordManager.VerifyPassword(request.Password, hashedPassword);

            if (!isLoginPasswordValid)
            {
                return BadRequest("Invalid password!");
            }

            var token = _jwtTokenHelper.GenerateToken(user.Username, user.Role);

            var loginResponse = new LoginResponse
            {
                Token = token,
                Message = "Successfully logged in!"
            };

            return Ok(loginResponse);
        }
    }
}