using ProjectManager.Data.Models;
using ProjectManager.DTOs.UserDTO;

namespace ProjectManager.Services
{
    public interface IAuthService
    {
        public Task<User> RegisterUser (RegisterUserDto registerUserDto);
    }
}
