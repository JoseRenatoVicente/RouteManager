﻿using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Entities;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.API.Services;

namespace Teams.API.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : BaseController
    {
        private readonly ITeamService _teamsService;

        public TeamsController(INotifier notifier, ITeamService teamsService) : base(notifier)
        {
            _teamsService = teamsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeam()
        {
            return Ok(await _teamsService.GetTeamsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(string id)
        {
            var team = await _teamsService.GetTeamByIdAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(string id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            return await CustomResponseAsync(await _teamsService.UpdateTeamAsync(team));
        }

        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            return await CustomResponseAsync(await _teamsService.AddTeamAsync(team));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(string id)
        {
            var team = await GetTeam(id);
            if (team == null)
            {
                return NotFound();
            }
            await _teamsService.RemoveTeamAsync(id);

            return NoContent();
        }
    }
}