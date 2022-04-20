﻿using RouteManager.Domain.Entities;
using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.Domain.Validations;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.API.Repository;

namespace Teams.API.Services
{
    public class TeamService : BaseService, ITeamService
    {
        private readonly GatewayService _gatewayService;
        private readonly ITeamRepository _teamRepository;
        private readonly ICityRepository _cityRepository;

        public TeamService(GatewayService gatewayService, ITeamRepository teamRepository, ICityRepository cityRepository, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
            _teamRepository = teamRepository;
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync() =>
            await _teamRepository.GetAllAsync();

        public async Task<Team> GetTeamByIdAsync(string id) =>
            await _teamRepository.FindAsync(c => c.Id == id);

        public async Task<Team> AddTeamAsync(Team team)
        {
            string peopleIds = "";

            foreach (var item in team.People)
            {
                peopleIds += item.Id + ",";
            }

            team.People = await _gatewayService.GetFromJsonAsync<IEnumerable<Person>>("People/api/People/list/" + peopleIds);
            if (team.People == null)
            {
                Notification("Pessoas não encontradas no sistema");
                return team;
            }

            team.City = await _cityRepository.FindAsync(c => c.Id == team.City.Id);
            if (team.City == null)
            {
                Notification("Cidade não encontrada no sistema");
                return team;
            }

            if (!ExecuteValidation(new TeamValidation(), team)) return team;

            return await _teamRepository.AddAsync(team);
        }

        public async Task<Team> UpdateTeamAsync(Team team)
        {
            if (!ExecuteValidation(new TeamValidation(), team)) return team;

            return await _teamRepository.UpdateAsync(team);
        }

        public async Task RemoveTeamAsync(Team team) =>
            await _teamRepository.RemoveAsync(team);

        public async Task RemoveTeamAsync(string id) =>
            await _teamRepository.RemoveAsync(id);
    }
}
