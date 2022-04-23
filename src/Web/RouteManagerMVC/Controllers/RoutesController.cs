using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Entities;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using RouteManagerMVC.Services;
using System;
using System.Threading.Tasks;

namespace RouteManagerMVC.Controllers
{
    public class RoutesController : MVCBaseController
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        public async Task<IActionResult> BatchRouteUpload(IFormFile file)
        {
            return View(await _routeService.RouteUpload(file));
        }

        public async Task<IActionResult> Import(string id)
        {
            return View(await _routeService.GetRouteByIdAsync(id));
        }

        public async Task<IActionResult> ExportToDocx(ReportRouteViewModel reportRoute)
        {
            return File(await _routeService.ExportToDocx(reportRoute.UploadRequest), "application/octet-stream", "Routes" + DateTime.Now.ToString("dd_MM_yyyy") + ".docx");
        }


        public async Task<IActionResult> Index()
        {
            return View(await _routeService.GetRoutesAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var route = await _routeService.GetRouteByIdAsync(id);

            if (route == null) return NotFound();

            return View(route);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OS,Base,Service,Address,Id")] Route route)
        {
            if (ModelState.IsValid)
            {
                await _routeService.AddRouteAsync(route);

                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _routeService.GetRouteByIdAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OS,Base,Service,Address,Id")] Route route)
        {
            if (id != route.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    await _routeService.UpdateRouteAsync(route);
     
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _routeService.GetRouteByIdAsync(id);

            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _routeService.RemoveRouteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
