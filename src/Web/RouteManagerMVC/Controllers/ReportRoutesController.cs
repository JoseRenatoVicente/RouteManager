using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using RouteManagerMVC.Services;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace RouteManagerMVC.Controllers
{
    public class ReportRoutesController : MVCBaseController
    {
        private readonly IReportRouteService _routeService;

        public ReportRoutesController(IReportRouteService routeService, INotifier notifier) : base(notifier)
        {
            _routeService = routeService;
        }

        public async Task<IActionResult> BatchRouteUpload(IFormFile file)
        {
            var report = await _routeService.RouteUpload(file);

            if (await IsOperationValid())
            {
                return RedirectToAction("Index");
            }
            TempData["Errors"] = await GetErrors();

            return await CustomResponseAsync(report);
        }

        public async Task<IActionResult> Import(string id)
        {
            return View(await _routeService.GetRouteByIdAsync(id));
        }

        public async Task<IActionResult> ExportToDocx(ReportRouteViewModel reportRoute)
        {
            var report = await _routeService.GetRouteByIdAsync(reportRoute.UploadRequest.ExcelFileId);
            reportRoute.Cities = report.Cities;
            reportRoute.Teams = report.Teams;
            reportRoute.ExcelFile = report.ExcelFile;

            var fileDocx = await _routeService.ExportToDocx(reportRoute.UploadRequest);

            if (await IsOperationValid())
            {
                return File(fileDocx, "application/octet-stream", "Routes" + DateTime.Now.ToString("dd_MM_yyyy") + ".docx");
            }
            TempData["Errors"] = await GetErrors();

            return View("Import", reportRoute);
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
            var route = await _routeService.GetRouteByIdAsync(id);

            await _routeService.RemoveRouteAsync(id);

            return await CustomResponseAsync(route);
        }
    }
}
