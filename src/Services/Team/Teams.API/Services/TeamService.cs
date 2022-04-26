using RouteManager.Domain.Entities;
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
        private readonly IPersonService _personService;

        public TeamService(GatewayService gatewayService, ITeamRepository teamRepository, ICityRepository cityRepository, IPersonService personService, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
            _teamRepository = teamRepository;
            _cityRepository = cityRepository;
            _personService = personService;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync() =>
            await _teamRepository.GetAllAsync();

        public async Task<Team> GetTeamByIdAsync(string id) =>
            await _teamRepository.FindAsync(c => c.Id == id);

        public async Task<IEnumerable<Team>> GetTeamByNameCityAsync(string nameCity) =>
            await _teamRepository.FindAllAsync(c => c.City.Name == nameCity);

        public async Task<IEnumerable<Team>> GetTeamByCityIdAsync(string id) =>
            await _teamRepository.FindAllAsync(c => c.City.Id == id);

        public async Task<Team> AddTeamAsync(Team team)
        {
            string peopleIds = "";

            if (team.People == null)
            {
                Notification("Nenhuma pessoa selecionada, escolha uma");
                return team;
            }

            foreach (var item in team.People)
            {
                peopleIds += item.Id + ",";
            }

            team.People = await _personService.GetPersonsByIdsAsync(peopleIds);
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
            string peopleIds = "";

            if (team.People == null)
            {
                Notification("Nenhuma pessoa selecionada, escolha uma");
                return team;
            }

            foreach (var item in team.People)
            {
                peopleIds += item.Id + ",";
            }

            team.People = await _personService.GetPersonsByIdsAsync(peopleIds);
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

            return !ExecuteValidation(new TeamValidation(), team) ? team : await _teamRepository.UpdateAsync(team);
        }

        public async Task RemoveTeamAsync(Team team) =>
            await _teamRepository.RemoveAsync(team);

        public async Task RemoveTeamAsync(string id) =>
            await _teamRepository.RemoveAsync(id);
    }
}
