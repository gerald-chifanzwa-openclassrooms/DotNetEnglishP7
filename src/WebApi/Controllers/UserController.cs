using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController, Authorize]
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

        /// <summary>
        /// Add user endpoint
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost("/user/add")]
        public async Task<IActionResult> AddUserAsync([FromBody][CustomizeValidator(RuleSet = "Create")] UserModel userModel)
        {
            try
            {
                var user = await _usersService.AddUser(userModel);
                return Ok(user);
            }
            catch (UsernameAlreadyTakenException ex)
            {
                // Username already asigned to another user in database
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get single user endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/user/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await _usersService.GetUser(id);
            return Ok(user);
        }

        /// <summary>
        /// Update user endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut("/user/update/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody][CustomizeValidator(RuleSet = "Update")] UserModel userModel)
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

        /// <summary>
        /// Delete user endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/user/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var user = await _usersService.DeleteUser(id);
            return Ok(user);
        }
    }
}