using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Data.Models;
using OnlineExamSystem.Data;

namespace OnlineExamSystem.Services
{
    public class UserService
    {
        private readonly OnlineExamSystemContext _dbContext;
        private readonly DbSet<User> _users;

        public UserService(OnlineExamSystemContext context)
        {
            _dbContext = context;
            _users = context.Users;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _users.FirstOrDefaultAsync(e => e.Id == id);

            if (user == null)
            {
                throw new NullReferenceException("User does not exist!");
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _users.ToListAsync();
        }

        public async Task<Guid> CreateUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> EditUser(Guid id, string newUsername)
        {
            var user = await GetUserById(id);
            var usernameExists = _users.Any(u => u.Username.Equals(newUsername));

            if (usernameExists)
            {
                throw new InvalidOperationException("Username is already taken!");
            }

            user.Username = newUsername;

            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await GetUserById(id);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}