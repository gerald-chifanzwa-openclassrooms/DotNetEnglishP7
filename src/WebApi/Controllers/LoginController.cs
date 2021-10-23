using System.Threading.Tasks;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Login endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/login"), AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _authenticationService.SignIn(model.UserName, model.Password);

            // Handle results from authentication service
            return result switch
            {
                AuthenticationResult.SuccessAuthenticationResult successResult => Ok(result),
                AuthenticationResult.FailureAuthenticationResult failure => Unauthorized(failure.Reason),
                _ => StatusCode(500, "Error processing your request")
            };
        }
    }
}