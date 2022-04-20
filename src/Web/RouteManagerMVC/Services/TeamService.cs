using RouteManager.Domain.Entities;
using RouteManager.Domain.Services;
using RouteManagerMVC.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface ITeamService
    {
        Task<ResponseResult> AddTeamAsync(Team team);
        Task<Team> GetTeamByIdAsync(string id);
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task RemoveTeamAsync(string id);
        Task<ResponseResult> UpdateTeamAsync(Team team);
    }

    public class TeamService : ITeamService
    {
        private readonly GatewayService _gatewayService;

        public TeamService(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<Team>>("Teams/api/Teams");
        }

        public async Task<Team> GetTeamByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<Team>("Teams/api/Teams/" + id);
        }

        public async Task<ResponseResult> AddTeamAsync(Team team)
        {
            return await _gatewayService.PostAsync<ResponseResult>("Teams/api/Teams/", team);
        }

        public async Task<ResponseResult> UpdateTeamAsync(Team team)
        {
            return await _gatewayService.PutAsync<ResponseResult>("Teams/api/Teams/" + team.Id, team);
        }

        public async Task RemoveTeamAsync(string id)
        {
            await _gatewayService.DeleteAsync("Teams/api/Teams/" + id);
        }

    }
}