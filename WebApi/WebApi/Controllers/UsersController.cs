using System;
using System.Collections.Generic;
using DOMAIN.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(
            [FromQuery] string name,
            string userName,
            string email,
            DateTime initial,
            DateTime final)
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
    }
}