using Identity.API.Models;
using Identity.API.Repository;
using RouteManager.Domain.Entities.Identity;
using RouteManager.Domain.Services.Base;
using RouteManager.Domain.Validations.Identity;
using RouteManager.WebAPI.Core.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, INotifier notifier) : base(notifier)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<User>> GetUsersAsync() =>
            await _userRepository.GetAllAsync();

        public async Task<User> GetUserByLoginAsync(string userName) =>
            await _userRepository.FindAsync(c => c.UserName == userName);

        public async Task<User> GetUserByIdAsync(string id) =>
            await _userRepository.FindAsync(c => c.Id == id);

        public async Task<User> AddUserAsync(User user)
        {
            if (await _userRepository.FindAsync(c => c.UserName == user.UserName || c.Email == user.Email) != null)
            {
                Notification("This username cannot be used");
                return null;
            }

            user.Role = await _roleRepository.FindAsync(c => c.Id == user.Role.Id);

            if (!ExecuteValidation(new UserValidation(), user)) return null;

            var passwordResult = await CreatePasswordHashAsync(user.Password);

            user.Password = passwordResult.passwordHash;
            user.PasswordSalt = passwordResult.passwordSalt;

            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> PasswordSignInAsync(UserLogin userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.UserName) || string.IsNullOrEmpty(userLogin.Password))
                return false;

            var user = await _userRepository.FindAsync(c => c.UserName == userLogin.UserName);

            return user == null ? false : await VerifyPasswordHashAsync(userLogin.Password, user.Password, user.PasswordSalt);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }
        public async Task RemoveUserAsync(string id) =>
            await _userRepository.RemoveAsync(id);

        private async Task<(string passwordSalt, string passwordHash)> CreatePasswordHashAsync(string password)
        {
            using (var hmac = new HMACSHA256())
            {
                return (Convert.ToBase64String(hmac.Key),
                        Convert.ToBase64String(await hmac.ComputeHashAsync(
                            new MemoryStream(Convert.FromBase64String(password)
                        ))));
            }
        }


        private async Task<bool> VerifyPasswordHashAsync(string password, string storedHash, string storedSalt)
        {
            Console.WriteLine(password);
            using (var hmac = new HMACSHA256(Convert.FromBase64String(storedSalt)))
            {
                string computedHash = Convert.ToBase64String(await hmac.ComputeHashAsync(new MemoryStream(Convert.FromBase64String(password))));

                if (storedHash.Equals(computedHash)) return true;
            }
            return false;
        }
    }
}
