using RouteManager.Domain.Services;
using RouteManagerMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface ICityService
    {
        Task<CityViewModel> AddCityAsync(CityViewModel city);
        Task<CityViewModel> GetCityByIdAsync(string id);
        Task<IEnumerable<CityViewModel>> GetCitysAsync();
        Task RemoveCityAsync(string id);
        Task<CityViewModel> UpdateCityAsync(CityViewModel city);
    }

    public class CityService : ICityService
    {
        private readonly GatewayService _gatewayService;

        public CityService(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<CityViewModel>> GetCitysAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<CityViewModel>>("Teams/api/Cities/");
        }

        public async Task<CityViewModel> GetCityByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<CityViewModel>("Teams/api/Cities/" + id);
        }

        public async Task<CityViewModel> AddCityAsync(CityViewModel city)
        {
            return await _gatewayService.PostAsync<CityViewModel>("Teams/api/Cities/", city);
        }

        public async Task<CityViewModel> UpdateCityAsync(CityViewModel city)
        {
            return await _gatewayService.PutAsync<CityViewModel>("Teams/api/Cities/" + city.Id, city);
        }

        public async Task RemoveCityAsync(string id)
        {
            await _gatewayService.DeleteAsync("Teams/api/Cities/" + id);
        }

    }
}