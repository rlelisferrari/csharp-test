using System;
using System.Collections.Generic;
using DATA.Contexts;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IUserRepository userRepository;

        public UsersController(AppDbContext context, IUserRepository userRepository)
        {
            this.context = context;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get([FromQuery] string name, string userName, string email, DateTime initial, DateTime final)
        {
            return Ok(
                this.userRepository.FindAll(
                    item => (userName == null || item.UserName == userName)
                            && (name == null || item.Name.Contains(name))
                            && (email == null || item.Email == email)
                            && (initial == DateTime.MinValue
                                || final == DateTime.MinValue
                                || initial <= item.RegistrationDate
                                && item.RegistrationDate <= final)));
        }

        [HttpPost("singup")]
        public ActionResult Post([FromBody] User user)
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