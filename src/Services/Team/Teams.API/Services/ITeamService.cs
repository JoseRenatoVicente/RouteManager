using RouteManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Teams.API.Services
{
    public interface ITeamService
    {
        Task<Team> AddTeamAsync(Team team);
        Task<Team> GetTeamByIdAsync(string id);
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task RemoveTeamAsync(string id);
        Task RemoveTeamAsync(Team team);
        Task<Team> UpdateTeamAsync(Team team);
    }
}