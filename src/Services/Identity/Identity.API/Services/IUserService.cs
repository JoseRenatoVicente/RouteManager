using Identity.API.Models;
using RouteManager.Domain.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByLoginAsync(string login);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> PasswordSignInAsync(UserLogin userLogin);
        Task<User> ChangePasswordCurrentUser(ChangePasswordCurrentUserViewModel changePassword);
        Task DisableUserAsync(string id);
        Task<User> UpdateUserAsync(User user);
    }
}