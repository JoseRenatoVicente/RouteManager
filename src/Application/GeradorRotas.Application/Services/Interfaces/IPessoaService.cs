using GeradorRotas.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeradorRotas.Application.Services.Interfaces
{
    public interface IPessoaService
    {
        Task AddPessoaAsync(Pessoa pessoa);
        Task<Pessoa> GetPessoaByIdAsync(string id);
        Task<IEnumerable<Pessoa>> GetPessoasAsync();
        Task RemovePessoaAsync(string id);
        Task UpdatePessoaAsync(Pessoa pessoa);
    }
}