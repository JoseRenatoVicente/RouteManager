using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.Domain.Entities.v1;

namespace Teams.API.Services
{
    public interface ITeamService
    {
        Task<Team> AddTeamAsync(Team team);
        Task<Team> GetTeamByIdAsync(string id);
        Task<IEnumerable<Team>> GetTeamByNameCityAsync(string nameCity);
        Task<IEnumerable<Team>> GetTeamByCityIdAsync(string id);
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task RemoveTeamAsync(string id);
        Task RemoveTeamAsync(Team team);
        Task<Team> UpdateTeamAsync(Team team);
    }
}