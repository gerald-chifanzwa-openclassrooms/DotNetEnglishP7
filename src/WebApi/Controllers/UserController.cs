using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UserController(IUsersService usersServie)
        {
            _usersService = usersServie;
        }

        [HttpGet("/user/list")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersService.GetUsers();
            return Ok(users);
        }

        [HttpPost("/user/add")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserModel userModel)
        {
            try
            {
                var user = await _usersService.AddUser(userModel);
                return Ok(user);
            }
            catch (UsernameAlreadyTakenException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/user/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await _usersService.GetUser(id);
            return Ok(user);
        }

        [HttpPost("/user/update/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserModel userModel)
        {
            try
            {
                var user = await _usersService.UpdateUser(id, userModel);
                return Ok(user);
            }
            catch (UsernameAlreadyTakenException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/user/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var user = await _usersService.DeleteUser(id);
            return Ok(user);
        }
    }
}