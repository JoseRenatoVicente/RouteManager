using RouteManager.Domain.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface IRoleService
    {

        Task<IEnumerable<Claim>> GetCurrentClaimsAsync();
        Task<Role> AddRoleAsync(Role role);
        Task<Role> GetRoleByDescriptionAsync(string description);
        Task<Role> GetRoleByIdAsync(string id);
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<bool> RemoveRoleAsync(Role role);
        Task<bool> RemoveRoleAsync(string id);
        Task<Role> UpdateRoleAsync(Role role);
    }
}