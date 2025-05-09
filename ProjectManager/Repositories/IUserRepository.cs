using ProjectManager.Data.Models;

namespace ProjectManager.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserByUsername(string username);
        public Task<User> AddUser(User user);
    }
}
