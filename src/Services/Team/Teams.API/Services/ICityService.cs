using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.Domain.Entities.v1;

namespace Teams.API.Services
{
    public interface ICityService
    {
        Task<City> AddCityAsync(City city);
        Task<City> GetCityByIdAsync(string id);
        Task<IEnumerable<City>> GetCitysAsync();
        Task<bool> RemoveCityAsync(City city);
        Task<bool> RemoveCityAsync(string id);
        Task<City> UpdateCityAsync(City city);
    }
}