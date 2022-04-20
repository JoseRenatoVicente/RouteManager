using RouteManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Teams.API.Services
{
    public interface ICityService
    {
        Task<City> AddCityAsync(City city);
        Task<City> GetCityByIdAsync(string id);
        Task<IEnumerable<City>> GetCitysAsync();
        Task RemoveCityAsync(City city);
        Task RemoveCityAsync(string id);
        Task<City> UpdateCityAsync(City city);
    }
}