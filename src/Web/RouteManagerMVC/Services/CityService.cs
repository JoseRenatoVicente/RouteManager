using RouteManager.Domain.Entities;
using RouteManager.Domain.Services;
using RouteManagerMVC.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface ICityService
    {
        Task<ResponseResult> AddCityAsync(City city);
        Task<City> GetCityByIdAsync(string id);
        Task<IEnumerable<City>> GetCitysAsync();
        Task RemoveCityAsync(string id);
        Task<ResponseResult> UpdateCityAsync(City city);
    }

    public class CityService : ICityService
    {
        private readonly GatewayService _gatewayService;

        public CityService(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<City>> GetCitysAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<City>>("Teams/api/Cities/");
        }

        public async Task<City> GetCityByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<City>("Teams/api/Cities/" + id);
        }

        public async Task<ResponseResult> AddCityAsync(City city)
        {
            return await _gatewayService.PostAsync<ResponseResult>("Teams/api/Cities/", city);
        }

        public async Task<ResponseResult> UpdateCityAsync(City city)
        {
            return await _gatewayService.PutAsync<ResponseResult>("Teams/api/Cities/" + city.Id, city);
        }

        public async Task RemoveCityAsync(string id)
        {
            await _gatewayService.DeleteAsync("Teams/api/Cities/" + id);
        }

    }
}