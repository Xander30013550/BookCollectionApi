using BookUserApi.Model;
using BookUserApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookUserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var existing = await _userService.GetByEmailAsync(user.Email);
            if (existing != null)
                return BadRequest("User already exists.");

            await _userService.CreateAsync(user);
            return Ok("User registered.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            var found = await _userService.GetByEmailAsync(user.Email);
            if (found == null || found.Password != user.Password)
                return Unauthorized("Invalid credentials.");

            return Ok("Login successful");
        }
    }
}
