using GeradorRotas.Application.Services.Interfaces;
using GeradorRotas.Domain.Entities;
using GeradorRotas.Infrastructure.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeradorRotas.Application.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository; 
        }

        public async Task<IEnumerable<Cidade>> GetCidadesAsync() =>
            await _cidadeRepository.GetAllAsync();

        public async Task<Cidade> GetCidadeByIdAsync(string id) =>
            await _cidadeRepository.GetByIdAsync(id);

        public async Task AddCidadeAsync(Cidade cidade)
        {
            await _cidadeRepository.AddAsync(cidade);
        }

        public async Task UpdateCidadeAsync(Cidade cidade)
        {
            await _cidadeRepository.UpdateAsync(cidade);
        }

        public async Task RemoveCidadeAsync(string id)
        {
            await _cidadeRepository.DeleteAsync(id);
        }

    }
}
