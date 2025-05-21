using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Data.Models;

namespace ProjectManager.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectManagerDbContext _context;
        public UserRepository(ProjectManagerDbContext context)
        {
            _context = context;
        }
        
        public async Task<User> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
