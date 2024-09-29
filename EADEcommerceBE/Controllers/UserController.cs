using EADEcommerceBE.Models;
using EADEcommerceBE.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EADEcommerceBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var id = await _userRepository.Create(user);
            return new JsonResult(id.ToString());
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetSingleUser(string id)
        {
            var user = await _userRepository.GetSingleUser(ObjectId.Parse(id));
            return new JsonResult(user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, User user)
        {
            var User = await _userRepository.UpdateUser(ObjectId.Parse(id), user);
            return new JsonResult(User);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userRepository.DeleteUser(ObjectId.Parse(id));
            return new JsonResult(user);
        }
        [HttpGet("Fetch")]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userRepository.GetAllUsers();
            return new JsonResult(user);
        }
    }
}
