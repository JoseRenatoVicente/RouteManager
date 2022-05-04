using RouteManager.Domain.Core.Services;
using RouteManager.Domain.Core.Services.Base;
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
            return await _gatewayService.GetFromJsonAsync<IEnumerable<UserViewModel>>("Identity/api/v1/Users");
        }

        public async Task<UserViewModel> GetUserByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<UserViewModel>("Identity/api/v1/Users/" + id);
        }

        public async Task<UserRegister> AddUserAsync(UserRegister user)
        {
            await _gatewayService.PostAsync("Identity/api/v1/Users/", user);
            return user;
        }

        public async Task<UserViewModel> UpdateUserAsync(UserViewModel user)
        {

            await _gatewayService.PutAsync<UserViewModel>("Identity/api/v1/Users/" + user.Id, user);
            return user;
        }

        public async Task DisableUserAsync(string id)
        {
            await _gatewayService.DeleteAsync("Identity/api/v1/Users/" + id);
        }

    }
}
