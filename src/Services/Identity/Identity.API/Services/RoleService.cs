using Identity.Domain.Contracts.v1;
using Identity.Domain.Entities.v1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Services;

public class RoleService : IRoleService
{

    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public Task<Claim[]> GetCurrentClaimsAsync()
    {
        return Task.Run(() => new Claim[]
        {
            new("Funções"),
            new("Usuários"),
            new("Logs"),
            new("Rotas"),
            new("Relátorio Rotas"),
            new("Equipes"),
            new("Cidades"),
            new("Pessoas")

        });
    }

    public async Task<IEnumerable<Role>> GetRolesAsync() =>
        await _roleRepository.GetAllAsync();

    public async Task<Role> GetRoleByDescriptionAsync(string description) =>
        await _roleRepository.FindAsync(c => c.Description == description);

    public async Task<Role> GetRoleByIdAsync(string id) =>
        await _roleRepository.FindAsync(c => c.Id == id);

}