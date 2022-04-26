using Identity.API.Models;
using Identity.API.Repository;
using RouteManager.Domain.Entities.Enums;
using RouteManager.Domain.Entities.Identity;
using RouteManager.Domain.Identity.Extensions;
using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.Domain.Validations.Identity;
using RouteManager.WebAPI.Core.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly GatewayService _gatewayService;
        private readonly IAspNetUser _aspNetUser;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(GatewayService gatewayService, IAspNetUser aspNetUser, IUserRepository userRepository, IRoleRepository roleRepository, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
            _aspNetUser = aspNetUser;
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
            user.Role = await _roleRepository.FindAsync(c => c.Id == user.Role.Id);
            if (!ExecuteValidation(new UserValidation(), user)) return user;

            if (await _userRepository.FindAsync(c => c.UserName == user.UserName || c.Email == user.Email) != null)
            {
                Notification("Nome de usuário ou email indisponíveis");
                return null;
            }


            var passwordResult = await CreatePasswordHashAsync(user.Password);

            user.Password = passwordResult.passwordHash;
            user.PasswordSalt = passwordResult.passwordSalt;

            await _gatewayService.PostLogAsync(null, user, Operation.Create);

            return await _userRepository.AddAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var userBefore = await GetUserByIdAsync(user.Id);
            user.Password = userBefore.Password;
            user.PasswordSalt = userBefore.PasswordSalt;
            user.Role = userBefore.Role;

            if (userBefore == null)
            {
                Notification("Usuário não encontrado");
                return user;
            }

            if (await _userRepository.FindAsync(c =>
                                                    (c.UserName == user.UserName || c.Email == user.Email)
                                                        && c.Id != user.Id) != null)

            {
                Notification("Nome de usuário ou email indisponíveis");
                return null;
            }

            user.Role = await _roleRepository.FindAsync(c => c.Id == user.Role.Id);

            await _gatewayService.PostLogAsync(userBefore, user, Operation.Update);

            return !ExecuteValidation(new UserValidation(), user) ? user : await _userRepository.UpdateAsync(user);
        }

        public async Task<User> ChangePasswordUser(ChangePasswordUserViewModel changePassword)
        {
            var user = await GetUserByIdAsync(changePassword.UserId);
            if (user == null)
            {
                Notification("Usuário não encontrado");
                return user;
            }

            return await ChangePassword(user, changePassword.UserId, changePassword.Password);
        }

        public async Task<User> ChangePasswordCurrentUser(ChangePasswordCurrentUserViewModel changePassword)
        {
            var user = await GetUserByIdAsync(changePassword.UserId);
            if (user == null)
            {
                Notification("Usuário não encontrado");
                return user;
            }
            if (!await PasswordSignInAsync(new UserLogin { UserName = user.UserName, Password = changePassword.CurrentPassword }))
            {
                Notification("Senha Atual incorreta");
                return user;
            }

            return await ChangePassword(user, changePassword.UserId, changePassword.Password);
        }

        public async Task<User> ChangePassword(User user, string userId, string password)
        {
            user.Password = password;

            if (!ExecuteValidation(new UserValidation(), user)) return user;

            var passwordResult = await CreatePasswordHashAsync(user.Password);
            user.Password = passwordResult.passwordHash;
            user.PasswordSalt = passwordResult.passwordSalt;

            return await _userRepository.UpdateAsync(user);
        }



        public async Task DisableUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);
            user.Active = false;

            if (user.Id == _aspNetUser.GetUserId())
            {
                Notification("Não é possivel excluir o proprio usuário logado nessa sessão");
                return;
            }            

            await UpdateUserAsync(user);

        }

        public async Task<bool> PasswordSignInAsync(UserLogin userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.UserName) || string.IsNullOrEmpty(userLogin.Password))
                return false;

            var user = await _userRepository.FindAsync(c => c.UserName == userLogin.UserName && c.Active);

            return user == null ? false : await VerifyPasswordHashAsync(userLogin.Password, user.Password, user.PasswordSalt);
        }

        private async Task<(string passwordSalt, string passwordHash)> CreatePasswordHashAsync(string password)
        {
            using (var hmac = new HMACSHA256())
            {
                return (Convert.ToBase64String(hmac.Key),
                        Convert.ToBase64String(await hmac.ComputeHashAsync(
                            new MemoryStream(Encoding.ASCII.GetBytes(password)
                        ))));
            }
        }


        private async Task<bool> VerifyPasswordHashAsync(string password, string storedHash, string storedSalt)
        {
            Console.WriteLine(password);
            using (var hmac = new HMACSHA256(Convert.FromBase64String(storedSalt)))
            {
                string computedHash = Convert.ToBase64String(await hmac.ComputeHashAsync(new MemoryStream(Encoding.ASCII.GetBytes(password))));

                if (storedHash.Equals(computedHash)) return true;
            }
            return false;
        }
    }
}
