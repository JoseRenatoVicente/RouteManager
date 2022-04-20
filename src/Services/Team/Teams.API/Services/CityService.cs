using RouteManager.Domain.Entities;
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
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository, INotifier notifier) : base(notifier)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<City>> GetCitysAsync() =>
            await _cityRepository.GetAllAsync();

        public async Task<City> GetCityByIdAsync(string id) =>
            await _cityRepository.FindAsync(c => c.Id == id);

        public async Task<City> AddCityAsync(City city)
        {
            if (!ExecuteValidation(new CityValidation(), city)) return city;

            return await _cityRepository.AddAsync(city);
        }

        public async Task<City> UpdateCityAsync(City city)
        {
            if (!ExecuteValidation(new CityValidation(), city)) return city;

            return await _cityRepository.UpdateAsync(city);
        }

        public async Task RemoveCityAsync(City city) =>
            await _cityRepository.RemoveAsync(city);

        public async Task RemoveCityAsync(string id) =>
            await _cityRepository.RemoveAsync(id);

    }
}
