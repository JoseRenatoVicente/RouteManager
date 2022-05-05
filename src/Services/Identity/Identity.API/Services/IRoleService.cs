using Identity.Domain.Entities.v1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Services;

public interface IRoleService
{

    Task<Claim[]> GetCurrentClaimsAsync();
    Task<Role> GetRoleByDescriptionAsync(string description);
    Task<Role> GetRoleByIdAsync(string id);
    Task<IEnumerable<Role>> GetRolesAsync();
}