using DATA.Contexts;
using DOMAIN.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UsersControllers : Controller
    {
        private readonly AppDbContext context;
        private readonly IUserRepository userRepository;

        public UsersControllers(AppDbContext context, IUserRepository userRepository)
        {
            this.context = context;
            this.userRepository = userRepository;
        }
    }
}