using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Threading.Tasks;
using Teams.API.Services;
using Teams.Domain.Commands.v1.Teams.Create;
using Teams.Domain.Commands.v1.Teams.Delete;
using Teams.Domain.Commands.v1.Teams.Update;

namespace Teams.API.Controllers.v1;

[Route("api/v1/[controller]")]
public class TeamsController : BaseController
{
    private readonly ITeamService _teamsService;

    public TeamsController(ITeamService teamsService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _teamsService = teamsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeam()
    {
        return Ok(await _teamsService.GetTeamsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeam(string id)
    {
        var team = await _teamsService.GetTeamByIdAsync(id);

        if (team == null)
        {
            return NotFound();
        }

        return Ok(team);
    }

    [HttpGet("City/{nameCity}")]
    public async Task<IActionResult> GetTeamByNameCity(string nameCity)
    {
        var team = await _teamsService.GetTeamByNameCityAsync(nameCity);

        if (team == null)
        {
            return NotFound();
        }

        return Ok(team);
    }

    [Authorize(Roles = "Equipes")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTeam(string id, UpdateTeamCommand updateTeam)
    {
        if (id != updateTeam.Id)
        {
            return BadRequest();
        }

        return await CustomResponseAsync(updateTeam);
    }

    [Authorize(Roles = "Equipes")]
    [HttpPost]
    public async Task<IActionResult> PostTeam(CreateTeamCommand createTeam)
    {
        return await CustomResponseAsync(createTeam);
    }

    [Authorize(Roles = "Equipes")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam([FromRoute] DeleteTeamCommand deleteTeam)
    {
        return await CustomResponseAsync(deleteTeam);
    }
}