using RouteManager.Domain.Core.Services;
using RouteManagerMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services;

public interface ITeamService
{
    Task<TeamRequest> AddTeamAsync(TeamRequest team);
    Task<TeamRequest> GetTeamByIdAsync(string id);
    Task<IEnumerable<TeamRequest>> GetTeamsAsync();
    Task RemoveTeamAsync(string id);
    Task<TeamViewModel> UpdateTeamAsync(TeamViewModel team);
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
        return await _gatewayService.GetFromJsonAsync<IEnumerable<TeamRequest>>("Teams/api/v1/Teams");
    }

    public async Task<TeamRequest> GetTeamByIdAsync(string id)
    {
        return await _gatewayService.GetFromJsonAsync<TeamRequest>("Teams/api/v1/Teams/" + id);
    }

    public async Task<TeamRequest> AddTeamAsync(TeamRequest team)
    {
        return await _gatewayService.PostAsync<TeamRequest>("Teams/api/v1/Teams/", team);
    }

    public async Task<TeamViewModel> UpdateTeamAsync(TeamViewModel team)
    {
        await _gatewayService.PutAsync("Teams/api/v1/Teams/" + team.Team.Id, team.Team);
        return team;
    }

    public async Task RemoveTeamAsync(string id)
    {
        await _gatewayService.DeleteAsync("Teams/api/v1/Teams/" + id);
    }

}