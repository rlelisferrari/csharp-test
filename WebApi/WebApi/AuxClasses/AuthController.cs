using System;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApi.AuxClasses
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("singin")]
        public IActionResult Singin(string userName, string password)
        {
            var user = this.userRepository.Find(item => item.UserName == userName && item.Password == password);
            if (user == null)
                return NotFound("Invalid Username or password");

            var token = AuthenticationService.GenerateToken(user, this.configuration);
            return Ok(token);
        }

        [HttpPost("singup")]
        public ActionResult Singup([FromBody] User user)
        {
            try
            {
                if (user == null)
                    return NotFound();
                user.RegistrationDate = DateTime.Now;
                this.userRepository.Add(user);
                return Ok("User successfully registered");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}