using GeradorRotas.Application.Services.Interfaces;
using GeradorRotas.Domain.Entities;
using GeradorRotas.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeradorRotas.Application.Services
{
    public class EquipeService : IEquipeService
    {
        private readonly IEquipeRepository _equipeRepository;

        public EquipeService(IEquipeRepository equipeRepository)
        {
            _equipeRepository = equipeRepository;
        }

        public async Task<IEnumerable<Equipe>> GetEquipesAsync() =>
            await _equipeRepository.GetAllAsync(c => c.Cidade);

        public async Task<Equipe> GetEquipeByIdAsync(string id) =>
            await (await _equipeRepository.GetAllAsync(c => c.Cidade))
                .FirstOrDefaultAsync(m => m.Id == id);

        public async Task AddEquipeAsync(Equipe equipe)
        {
            await _equipeRepository.AddAsync(equipe);
        }

        public async Task UpdateEquipeAsync(Equipe equipe)
        {
            await _equipeRepository.UpdateAsync(equipe);
        }

        public async Task RemoveEquipeAsync(string id)
        {
            await _equipeRepository.DeleteAsync(id);
        }
    }
}
