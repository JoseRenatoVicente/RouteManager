using MongoDB.Driver;
using RouteManager.Domain.Core.Entities.Enums;
using RouteManager.Domain.Core.Services;
using RouteManager.Domain.Core.Services.Base;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;
using Teams.Domain.Validations.v1;

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
            await _gatewayService.PostLogAsync(null, city, Operation.Create);

            return !ExecuteValidation(new CityValidation(), city) ? city : await _cityRepository.AddAsync(city);
        }

        public async Task<City> UpdateCityAsync(City city)
        {
            var cityBefore = await GetCityByIdAsync(city.Id);

            if (cityBefore == null)
            {
                Notification("Cidade não encontrada");
                return city;
            }

            var filterDefinition = Builders<Team>.Filter.Eq(p => p.City.Id, cityBefore.Id);
            var updateDefinition = Builders<Team>.Update.Set(p => p.City, city);

            await _teamRepository.UpdateAllAsync(filterDefinition, updateDefinition);

            await _gatewayService.PostLogAsync(cityBefore, city, Operation.Update);

            return !ExecuteValidation(new CityValidation(), city) ? city : await _cityRepository.UpdateAsync(city);
        }

        public async Task<bool> RemoveCityAsync(string id)
        {
            var city = await GetCityByIdAsync(id);

            if (city == null)
            {
                Notification("Cidade não encontrada");
                return false;
            }

            return await RemoveCityAsync(city);
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



    }
}
