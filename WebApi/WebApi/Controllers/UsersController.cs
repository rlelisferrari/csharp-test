using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.Models;
using DOMAIN.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// return list of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<User>), 200)]
        public async Task<ActionResult<IEnumerable<User>>> Get(
            [FromQuery] string userName,
            string name,
            string email,
            DateTime initial,
            DateTime final)
        {
            var users = await this.userService.FindUsers(userName, name, email, initial, final);
            return Ok(users);
        }
    }
}