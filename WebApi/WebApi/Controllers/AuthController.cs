using System.Threading.Tasks;
using DOMAIN.Models;
using DOMAIN.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.AuxClasses;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserService userService;
        private readonly IConfiguration configuration;
        
        public AuthController(IConfiguration configuration, UserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
        }

        /// <summary>
        /// Login user: receive username and password
        /// </summary>
        /// <response code="200">return token JWT</response>
        /// <response code="500">Invalid Username or password</response>
        [HttpPost("singin")]
        public async Task<IActionResult> Singin(string userName, string password)
        {
            var user = await this.userService.FindUser(userName, password);
            var token = AuthenticationService.GenerateToken(user, this.configuration);
            return Ok(token);
        }

        /// <summary>
        /// register a new user
        /// </summary>
        /// <response code="200">User successfully registered</response>
        /// <response code="500">Invalid parameters</response>
        [HttpPost("singup")]
        public async Task<IActionResult> Singup([FromBody] User user)
        {
            await this.userService.Add(user);
            return Ok("User successfully registered");
        }
    }
}