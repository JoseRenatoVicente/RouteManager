using RouteManager.Domain.Services;
using RouteManagerMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface IRoleService
    {
        Task<RoleRequestViewModel> AddRoleAsync(RoleRequestViewModel role);
        Task<RoleRequestViewModel> GetRoleByIdAsync(string id);
        Task<IEnumerable<RoleRequestViewModel>> GetRolesAsync();

        Task<IEnumerable<ClaimViewModel>> GetCurrentClaimsAsync();
        Task RemoveRoleAsync(string id);
        Task<RoleRequestViewModel> UpdateRoleAsync(RoleRequestViewModel role);
    }

    public class RoleService : IRoleService
    {
        private readonly GatewayService _gatewayService;

        public RoleService(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<ClaimViewModel>> GetCurrentClaimsAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<ClaimViewModel>>("Identity/api/Roles/Claims/");
        }

        public async Task<IEnumerable<RoleRequestViewModel>> GetRolesAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<RoleRequestViewModel>>("Identity/api/Roles/");
        }
        public async Task<RoleRequestViewModel> GetRoleByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<RoleRequestViewModel>("Identity/api/Roles/" + id);
        }

        public async Task<RoleRequestViewModel> AddRoleAsync(RoleRequestViewModel role)
        {
            return await _gatewayService.PostAsync<RoleRequestViewModel>("Identity/api/Roles/", role);
        }

        public async Task<RoleRequestViewModel> UpdateRoleAsync(RoleRequestViewModel role)
        {
            return await _gatewayService.PutAsync<RoleRequestViewModel>("Identity/api/Roles/" + role.Id, role);
        }

        public async Task RemoveRoleAsync(string id)
        {
            await _gatewayService.DeleteAsync("Identity/api/Roles/" + id);
        }

    }
}
