/*********************************************** 
    User Controller
    All API end points of User Management
    Dilshan W.A.B. - IT21343216
 **********************************************/

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

        //Create new user
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is missing.");
            }
            var id = await _userRepository.Create(user);
            return Ok(new { Message = "User created successfully", UserId = id.ToString() });
        }

        //Return User using user id
        [HttpGet("getSingleUser/{id}")]
        public async Task<IActionResult> GetSingleUser(string id)
        {
            var user = await _userRepository.GetSingleUser(ObjectId.Parse(id));
            return new JsonResult(user);
        }

        //Update User using user id
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

        //Delete User using user Id
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

        //Fetch all users
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            // Convert ObjectId to string for UserId field in each user
            var userList = users.Select(user => new
            {
                userId = user.Id.ToString(),  // Convert ObjectId to string
                user.Name,
                user.Email,
                user.Address,
                user.Phone,
                user.UserType,
                user.IsWebUser,
                user.Username,
                user.AccountStatus,
                user.AvgRating
            });

            return new JsonResult(userList);
        }

        //Update account status using user id
        [HttpPut("updateAccountStatus/{id}")]
        public async Task<IActionResult> UpdateAccountStatus(string id, [FromBody] string accountStatus)
        {
            if (!ObjectId.TryParse(id, out var id2))
                return BadRequest("Invalid Status");

            var result = await _userRepository.UpdateAccountStatusById(id2, accountStatus);
            if (!result)
                return NotFound("Account status not found");

            return Ok("Account Status updated successfully");
        }

        //User Login
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
                UserId = user.Id.ToString(),
                user,
                Token = token
            });
        }
    }
}
