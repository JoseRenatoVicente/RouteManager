using Identity.API.Repository;
using RouteManager.Domain.Entities.Identity;
using RouteManager.Domain.Services.Base;
using RouteManager.Domain.Validations.Identity;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository, INotifier notifier) : base(notifier)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync() =>
            await _roleRepository.GetAllAsync();

        public async Task<Role> GetRoleByDescriptionAsync(string description) =>
    await _roleRepository.FindAsync(c => c.Description == description);

        public async Task<Role> GetRoleByIdAsync(string id) =>
            await _roleRepository.FindAsync(c => c.Id == id);

        public async Task<Role> AddRoleAsync(Role role)
        {
            if (!ExecuteValidation(new RoleValidation(), role)) return role;

            return await _roleRepository.AddAsync(role);
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            return await _roleRepository.UpdateAsync(role);
        }

        public async Task RemoveRoleAsync(Role role) =>
            await _roleRepository.RemoveAsync(role);

        public async Task RemoveRoleAsync(string id) =>
            await _roleRepository.RemoveAsync(id);
    }
}
