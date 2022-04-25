using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using RouteManagerMVC.Services;
using System.Threading.Tasks;

namespace RouteManagerMVC.Controllers
{
    public class TeamsController : MVCBaseController
    {
        private readonly ICityService _cityService;
        private readonly IPersonService _personService;
        private readonly ITeamService _teamService;

        public TeamsController(ICityService cityService, IPersonService personService, ITeamService teamService, INotifier notifier) : base(notifier)
        {
            _cityService = cityService;
            _personService = personService;
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _teamService.GetTeamsAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new TeamViewModel
            {
                Cities = await _cityService.GetCitysAsync(),
                People = await _personService.GetPersonsAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Team, PeopleIds")] TeamViewModel teamViewModel)
        {
            teamViewModel.Team.People = await _personService.GetPersonsByIdsAsync(teamViewModel.PeopleIds);

            return await CustomResponseAsync(await _teamService.AddTeamAsync(teamViewModel.Team));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = new TeamViewModel
            {
                Cities = await _cityService.GetCitysAsync(),
                People = await _personService.GetPersonsAsync()
            };

            viewModel.Team = await _teamService.GetTeamByIdAsync(id);

            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TeamViewModel teamViewModel)
        {
            teamViewModel.Team.Id = id;
            teamViewModel.Team.People = await _personService.GetPersonsByIdsAsync(teamViewModel.PeopleIds);

            return await CustomResponseAsync(await _teamService.UpdateTeamAsync(teamViewModel.Team));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);

            await _teamService.RemoveTeamAsync(id);

            return await CustomResponseAsync(team);
        }
    }
}
