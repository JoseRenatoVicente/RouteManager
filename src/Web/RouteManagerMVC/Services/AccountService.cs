using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Models;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface IAccountService
    {
        Task<ChangePasswordViewModel> ChangePasswordAsync(ChangePasswordViewModel changePassword);
        Task<UserUpdate> GetCurrentUser();
        Task<UserUpdate> UpdateCurrentUserAsync(UserUpdate user);
    }

    public class AccountService : BaseService, IAccountService
    {
        private readonly GatewayService _gatewayService;

        public AccountService(GatewayService gatewayService, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
        }

        public async Task<UserUpdate> GetCurrentUser()
        {
            return await _gatewayService.GetFromJsonAsync<UserUpdate>("Identity/api/Account");
        }

        public async Task<UserUpdate> UpdateCurrentUserAsync(UserUpdate user)
        {
            var responseMessage = await _gatewayService.PutAsync("Identity/api/Account", user);

            if (!responseMessage.IsSuccessStatusCode)
            {
                foreach (var item in (await _gatewayService.DeserializeObjectResponse<ErrorResult>(responseMessage)).Errors)
                {
                    Notification(item);
                };
            }
            return user;
        }

        public async Task<ChangePasswordViewModel> ChangePasswordAsync(ChangePasswordViewModel changePassword)
        {
            var responseMessage = await _gatewayService.PostAsync("Identity/api/Account/ChangePassword", changePassword);

            if (!responseMessage.IsSuccessStatusCode)
            {
                foreach (var item in (await _gatewayService.DeserializeObjectResponse<ErrorResult>(responseMessage)).Errors)
                {
                    Notification(item);
                };
            }
            return changePassword;
        }
    }
}
