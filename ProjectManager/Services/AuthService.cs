using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Data.Models;
using ProjectManager.DTOs.UserDTO;
using ProjectManager.Helpers;
using ProjectManager.Middleware;
using ProjectManager.Repositories;

namespace ProjectManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
                _userRepository = userRepository;
        }

        public async Task<User?> RegisterUser(RegisterUserDto registerUserDto)
        {
            if (await _userRepository.GetUserByEmail(registerUserDto.Email) != null)
            {
                throw new UserAlreadyExistsException("Email is already in use.");
            }

            if (await _userRepository.GetUserByUsername(registerUserDto.Username) != null)
            {
                throw new UserAlreadyExistsException("Username is already taken.");
            }

            var (passwordHash, passwordSalt) = PasswordHelper.HashPassowrd(registerUserDto.Password);

            var user = new User
            {
                Username = registerUserDto.Username,
                Email = registerUserDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            return await _userRepository.AddUser(user);
        }
    }
}
