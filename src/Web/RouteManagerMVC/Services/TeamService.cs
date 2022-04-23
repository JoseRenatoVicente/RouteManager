using RouteManager.Domain.Services;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface ITeamService
    {
        Task<ResponseResult> AddTeamAsync(TeamRequest team);
        Task<TeamRequest> GetTeamByIdAsync(string id);
        Task<IEnumerable<TeamRequest>> GetTeamsAsync();
        Task RemoveTeamAsync(string id);
        Task<ResponseResult> UpdateTeamAsync(TeamRequest team);
    }

    public class TeamService : ITeamService
    {
        private readonly GatewayService _gatewayService;

        public TeamService(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<TeamRequest>> GetTeamsAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<TeamRequest>>("Teams/api/Teams");
        }

        public async Task<TeamRequest> GetTeamByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<TeamRequest>("Teams/api/Teams/" + id);
        }

        public async Task<ResponseResult> AddTeamAsync(TeamRequest team)
        {
            return await _gatewayService.PostAsync<ResponseResult>("Teams/api/Teams/", team);
        }

        public async Task<ResponseResult> UpdateTeamAsync(TeamRequest team)
        {
            return await _gatewayService.PutAsync<ResponseResult>("Teams/api/Teams/" + team.Id, team);
        }

        public async Task RemoveTeamAsync(string id)
        {
            await _gatewayService.DeleteAsync("Teams/api/Teams/" + id);
        }

    }
}