using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using RouteManagerMVC.Services;
using System.Threading.Tasks;

namespace RouteManagerMVC.Controllers
{
    public class RoutesController : MVCBaseController
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService, INotifier notifier) : base(notifier)
        {
            _routeService = routeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _routeService.GetRoutesAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _routeService.GetRouteByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RouteRequestViewModel route)
        {
            return await CustomResponseAsync(await _routeService.AddRouteAsync(route));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _routeService.GetRouteByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RouteRequestViewModel route)
        {
            route.Id = id;

            return await CustomResponseAsync(await _routeService.UpdateRouteAsync(route));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _routeService.GetRouteByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await _routeService.GetRouteByIdAsync(id);

            await _routeService.RemoveRouteAsync(person.Id);

            return await CustomResponseAsync(person);
        }
    }
}
