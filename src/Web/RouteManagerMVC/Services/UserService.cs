using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface IUserService
    {
        Task<UserRegister> AddUserAsync(UserRegister user);
        Task<UserViewModel> GetUserByIdAsync(string id);
        Task<IEnumerable<UserViewModel>> GetUsersAsync();
        Task DisableUserAsync(string id);
        Task<UserViewModel> UpdateUserAsync(UserViewModel user);
    }

    public class UserService : BaseService, IUserService
    {
        private readonly GatewayService _gatewayService;

        public UserService(GatewayService gatewayService, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<UserViewModel>>("Identity/api/Users");
        }

        public async Task<UserViewModel> GetUserByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<UserViewModel>("Identity/api/Users/" + id);
        }

        public async Task<UserRegister> AddUserAsync(UserRegister user)
        {
            await _gatewayService.PostAsync("Identity/api/Users/", user);
            return user;
        }

        public async Task<UserViewModel> UpdateUserAsync(UserViewModel user)
        {
            return await _gatewayService.PutAsync<UserViewModel>("Identity/api/Users/" + user.Id, user);
        }

        public async Task DisableUserAsync(string id)
        {
            await _gatewayService.DeleteAsync("Identity/api/Users/" + id);
        }

    }
}
