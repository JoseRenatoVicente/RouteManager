using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;

namespace Teams.API.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;

    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<IEnumerable<Team>> GetTeamsAsync() =>
        await _teamRepository.GetAllAsync();

    public async Task<Team> GetTeamByIdAsync(string id) =>
        await _teamRepository.FindAsync(c => c.Id == id);

    public async Task<IEnumerable<Team>> GetTeamByNameCityAsync(string nameCity) =>
        await _teamRepository.FindAllAsync(c => c.City.Name == nameCity);

    public async Task<IEnumerable<Team>> GetTeamByCityIdAsync(string id) =>
        await _teamRepository.FindAllAsync(c => c.City.Id == id);
}