using ProjectManager.Data.Models;

namespace ProjectManager.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
