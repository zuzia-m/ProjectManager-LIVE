using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DTOs.UserDTO;
using ProjectManager.Services;

namespace ProjectManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (registerUserDto.Password != registerUserDto.RepeatPassword)
            {
                return BadRequest(new {message = "Password do not match"});
            }

            var user = await _authService.RegisterUser(registerUserDto);

            return Ok(new { message = $"User {user.Username} successfully created" });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _authService.Authenticate(loginDto.Username, loginDto.Password);
            if (user is null) return Unauthorized(new { message = $"Invalid username or password." });

            //Generate JWT Token
            return Ok(); // return Token
        }
    }
}
