using GeradorRotas.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeradorRotas.Application.Services.Interfaces
{
    public interface ICidadeService
    {
        Task AddCidadeAsync(Cidade cidade);
        Task<Cidade> GetCidadeByIdAsync(string id);
        Task<IEnumerable<Cidade>> GetCidadesAsync();
        Task RemoveCidadeAsync(string id);
        Task UpdateCidadeAsync(Cidade cidade);
    }
}