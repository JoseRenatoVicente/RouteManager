using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;

namespace Teams.API.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<IEnumerable<City>> GetCitysAsync() =>
        await _cityRepository.GetAllAsync();

    public async Task<City> GetCityByIdAsync(string id) =>
        await _cityRepository.FindAsync(c => c.Id == id);


}