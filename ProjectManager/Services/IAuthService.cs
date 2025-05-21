using ProjectManager.Data.Models;
using ProjectManager.DTOs.UserDTO;

namespace ProjectManager.Services
{
    public interface IAuthService
    {
        public Task<User> RegisterUser (RegisterUserDto registerUserDto);
        public Task<User> Authenticate(string username, string password);
    }
}
