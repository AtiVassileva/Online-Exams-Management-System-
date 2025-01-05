using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.API.Attributes;
using OnlineExamSystem.API.Helpers;
using OnlineExamSystem.API.Models;
using OnlineExamSystem.Data;
using OnlineExamSystem.Data.Models;
using LoginRequest = OnlineExamSystem.API.Models.LoginRequest;
using RegisterRequest = OnlineExamSystem.API.Models.RegisterRequest;

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

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter valid data!");
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (existingUser != null)
            {
                return BadRequest("Username is already taken!");
            }

            if (!request.Password.Equals(request.ConfirmPassword, StringComparison.InvariantCulture))
            {
                return BadRequest("Passwords do not match!");
            }

            var user = new User
            {
                Username = request.Username,
                PasswordHash = _passwordManager.HashPassword(request.Password),
                Role = string.Concat(request.Role[0].ToString().ToUpper(), request.Role.AsSpan(1)), // will be removed when finishing FE
                CreatedAt = DateTime.UtcNow
            };

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!");
            }

            var registerResponse = new RegisterResponse
            {
                Message = "Successful registration!"
            };

            return Ok(registerResponse);
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

            HttpContext.Session.SetString("AuthToken", token);
            HttpContext.Session.SetString("UserRole", user.Role);

            return Ok(loginResponse);
        }

        [HttpPost("Logout")]
        [RequireAuthorization]
        public Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("AuthToken");
            HttpContext.Session.Remove("UserRole");

            var logoutResponse = new RegisterResponse
            {
                Message = "Logout successful!"
            };

            return Task.FromResult<IActionResult>(Ok(logoutResponse));
        }
    }
}