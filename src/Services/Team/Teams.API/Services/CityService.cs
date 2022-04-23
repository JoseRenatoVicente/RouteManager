using RouteManager.Domain.Entities;
using RouteManager.Domain.Entities.Enums;
using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.Domain.Validations;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.API.Repository;

namespace Teams.API.Services
{
    public class CityService : BaseService, ICityService
    {
        private readonly GatewayService _gatewayService;
        private readonly ICityRepository _cityRepository;
        private readonly ITeamRepository _teamRepository;

        public CityService(GatewayService gatewayService, ICityRepository cityRepository, ITeamRepository teamRepository, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
            _cityRepository = cityRepository;
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<City>> GetCitysAsync() =>
            await _cityRepository.GetAllAsync();

        public async Task<City> GetCityByIdAsync(string id) =>
            await _cityRepository.FindAsync(c => c.Id == id);

        public async Task<City> AddCityAsync(City city)
        {
            if (!ExecuteValidation(new CityValidation(), city)) return city;

            await _gatewayService.PostLogAsync(null, city, Operation.Create);

            return await _cityRepository.AddAsync(city);
        }

        public async Task<City> UpdateCityAsync(City city)
        {
            if (!ExecuteValidation(new CityValidation(), city)) return city;

            var cityBefore = await GetCityByIdAsync(city.Id);

            if (cityBefore == null)
            {
                Notification("Not found");
                return city;
            }

            await _gatewayService.PostLogAsync(cityBefore, city, Operation.Update);

            return await _cityRepository.UpdateAsync(city);
        }

        public async Task<bool> RemoveCityAsync(City city)
        {
            if (await _teamRepository.FindAsync(c => c.City.Id == city.Id) != null)
            {
                Notification("Essa cidade possui equipes vinculadas exclua primeiro a equipe para excluir a cidade");
                return false;
            }

            await _gatewayService.PostLogAsync(null, city, Operation.Delete);

            return await _cityRepository.RemoveAsync(city);
        }

        public async Task<bool> RemoveCityAsync(string id)
        {
            var city = await GetCityByIdAsync(id);

            if (city == null)
            {
                Notification("Not found");
                return false;
            }

            if (await _teamRepository.FindAsync(c => c.City.Id == city.Id) != null)
            {
                Notification("Essa cidade possui equipes vinculadas exclua primeiro a equipe para excluir a cidade");
                return false;
            }

            await _gatewayService.PostLogAsync(null, city, Operation.Delete);

            return await _cityRepository.RemoveAsync(id);
        }

    }
}
