using RouteManager.Application.Services.Interfaces;
using RouteManager.Domain.Entities;
using RouteManager.Infrastructure.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManager.Application.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository; 
        }

        public async Task<IEnumerable<City>> GetCidadesAsync() =>
            await _cidadeRepository.GetAllAsync();

        public async Task<City> GetCidadeByIdAsync(string id) =>
            await _cidadeRepository.GetByIdAsync(id);

        public async Task AddCidadeAsync(City cidade)
        {
            await _cidadeRepository.AddAsync(cidade);
        }

        public async Task UpdateCidadeAsync(City cidade)
        {
            await _cidadeRepository.UpdateAsync(cidade);
        }

        public async Task RemoveCidadeAsync(string id)
        {
            await _cidadeRepository.DeleteAsync(await GetCidadeByIdAsync(id));
        }

    }
}
