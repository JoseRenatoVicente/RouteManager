using RouteManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManager.Application.Services.Interfaces
{
    public interface ICidadeService
    {
        Task AddCidadeAsync(City cidade);
        Task<City> GetCidadeByIdAsync(string id);
        Task<IEnumerable<City>> GetCidadesAsync();
        Task RemoveCidadeAsync(string id);
        Task UpdateCidadeAsync(City cidade);
    }
}