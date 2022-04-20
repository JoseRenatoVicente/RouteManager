using RouteManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManager.Application.Services.Interfaces
{
    public interface IEquipeService
    {
        Task AddEquipeAsync(Equipe equipe);
        Task<Equipe> GetEquipeByIdAsync(string id);
        Task<IEnumerable<Equipe>> GetEquipesAsync();
        Task RemoveEquipeAsync(string id);
        Task UpdateEquipeAsync(Equipe equipe);
    }
}