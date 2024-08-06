using Journal.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserController() { }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok("GetUsers");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok($"GetUser {id}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            return Ok("$Create User");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updateUser)
        {
            return Ok($"Update User {id}");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok($"Delete User {id}");
        }
    }
