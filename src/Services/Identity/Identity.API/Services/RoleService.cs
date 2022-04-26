using Identity.API.Repository;
using MongoDB.Driver;
using RouteManager.Domain.Entities.Enums;
using RouteManager.Domain.Entities.Identity;
using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.Domain.Validations.Identity;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly GatewayService _gatewayService;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public RoleService(GatewayService gatewayService, IRoleRepository roleRepository, IUserRepository userRepository, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Claim>> GetCurrentClaimsAsync()
        {
            return new Claim[]
            {
                new("Funções"),
                new("Usuários"),
                new("Logs"),
                new("Rotas"),
                new("Relátorio Rotas"),
                new("Equipes"),
                new("Cidades"),
                new("Pessoas")

            };
        }

        public async Task<IEnumerable<Role>> GetRolesAsync() =>
            await _roleRepository.GetAllAsync();

        public async Task<Role> GetRoleByDescriptionAsync(string description) =>
            await _roleRepository.FindAsync(c => c.Description == description);

        public async Task<Role> GetRoleByIdAsync(string id) =>
            await _roleRepository.FindAsync(c => c.Id == id);

        public async Task<Role> AddRoleAsync(Role role)
        {
            if (role.Claims == null)
            {
                Notification("Nenhuma claim selecionada");
                return role;
            }

            await _gatewayService.PostLogAsync(null, role, Operation.Create);

            return !ExecuteValidation(new RoleValidation(), role) ? role : await _roleRepository.AddAsync(role);
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            if (!ExecuteValidation(new RoleValidation(), role)) return role;

            var roleBefore = await GetRoleByIdAsync(role.Id);

            if (roleBefore == null)
            {
                Notification("Função não encontrada");
                return role;
            }

            var filterDefinition = Builders<User>.Filter.Eq(p => p.Role.Id, role.Id);
            var updateDefinition = Builders<User>.Update.Set(p => p.Role, role);

            await _userRepository.UpdateAllAsync(filterDefinition, updateDefinition);

            await _gatewayService.PostLogAsync(roleBefore, role, Operation.Update);

            return await _roleRepository.UpdateAsync(role);
        }


        public async Task<bool> RemoveRoleAsync(Role role)
        {
            await _gatewayService.PostLogAsync(null, role, Operation.Delete);

            return await _roleRepository.RemoveAsync(role);
        }

        public async Task<bool> RemoveRoleAsync(string id)
        {
            var role = await GetRoleByIdAsync(id);

            if (role == null)
            {
                Notification("Função não encontrada");
                return false;
            }

            return await RemoveRoleAsync(role);
        }
    }
}
