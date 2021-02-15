using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DOMAIN.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;
        private readonly UserValidator userValidator;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.userValidator = new UserValidator();
        }

        public async Task<IEnumerable<User>> FindUsers(
            string userName,
            string name,
            string email,
            DateTime initialDateTime,
            DateTime finalDateTime)
        {
            return await this.userRepository.FindAllAsync(
                item => (userName == null || item.UserName == userName)
                        && (name == null || item.Name.Contains(name))
                        && (email == null || item.Email == email)
                        && (initialDateTime == DateTime.MinValue
                            || finalDateTime == DateTime.MinValue
                            || initialDateTime <= item.RegistrationDate
                            && item.RegistrationDate <= finalDateTime));
        }

        public async Task<User> FindUser(string userName, string password)
        {
            var user = await this.userRepository.FindAsync(
                item => item.UserName == userName && item.Password == password);

            if (user == null)
                throw new InvalidOperationException("Invalid Username or password");

            return user;
        }

        public async Task Add(User user)
        {
            if (!this.userValidator.IsValidUser(user))
                throw new InvalidOperationException(this.userValidator.ErrorMessage);

            user.RegistrationDate = DateTime.Now;
            await this.userRepository.AddAsyn(user);
        }

        private class UserValidator
        {
            public string ErrorMessage { get; private set; }

            public bool IsValidUser(User user)
            {
                if (user == null)
                {
                    ErrorMessage = "User is null";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(user.Name))
                {
                    ErrorMessage = "Invalid User name";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(user.UserName))
                {
                    ErrorMessage = "Invalid UserName";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    ErrorMessage = "Invalid User Password";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    ErrorMessage = "Invalid User Email";
                    return false;
                }

                ErrorMessage = "";
                return true;
            }
        }

        public void Dispose()
        {
            this.userRepository.Dispose();
        }
    }
}