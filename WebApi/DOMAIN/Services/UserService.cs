using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DOMAIN.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await this.userRepository.GetAllAsyn();
        }

        public void Dispose()
        {
            this.userRepository.Dispose();
        }
    }
}