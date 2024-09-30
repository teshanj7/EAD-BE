using EADEcommerceBE.Middleware;
using EADEcommerceBE.Models;
using EADEcommerceBE.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace EADEcommerceBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var id = await _userRepository.Create(user);
            return Ok(new { Message = "User created successfully", UserId = id.ToString() });
        }
        [HttpGet("getSingleUser/{id}")]
        public async Task<IActionResult> GetSingleUser(string id)
        {
            var user = await _userRepository.GetSingleUser(ObjectId.Parse(id));
            return new JsonResult(user);
        }
        [HttpPut("updateUser/{id}")]
        public async Task<IActionResult> UpdateUser(string id, User user)
        {
            if (!ObjectId.TryParse(id, out var id2))
                return BadRequest("Invalid User");

            var result = await _userRepository.UpdateUser(id2, user);
            if (!result)
                return NotFound("User not found.");
            
            return Ok("User updated successfully");
        }
        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!ObjectId.TryParse(id, out var id2))
                return BadRequest("Invalid User");

            var result = await _userRepository.DeleteUser(id2);
            if (!result)
                return NotFound("User not found");

            return Ok("User deleted successfully");
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return new JsonResult(users);
        }
        [HttpPut("updateAccountStatus/{id}")]
        public async Task<IActionResult> UpdateAccountStatus(string id, User user)
        {
            if (!ObjectId.TryParse(id, out var id2))
                return BadRequest("Invalid Status");

            var result = await _userRepository.UpdateAccountStatusById(id2, user);
            if (!result)
                return NotFound("Account status not found");

            return Ok("Account Status updated successfully");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userRepository.Login(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid email or password, or account is not activate." });
            }

            // Generate token
            var tokenMiddleware = new TokenMiddleware(new RequestDelegate(context => Task.CompletedTask), _configuration);
            var token = tokenMiddleware.GenerateToken(user);

            return Ok(new
            {
                Message = "Login successful",
                user.UserType,
                user,
                Token = token
            });
        }
    }
}
