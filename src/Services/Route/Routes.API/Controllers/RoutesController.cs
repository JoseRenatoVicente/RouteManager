using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Entities;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using Routes.API.DTO;
using Routes.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Routes.API.Controllers
{
    [Route("api/[controller]")]
    public class RoutesController : BaseController
    {
        private readonly IRouteService _routeService;
        public RoutesController(IRouteService routeService, INotifier notifier) : base(notifier)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Route>>> GetRoute()
        {
            return Ok(await _routeService.GetRoutesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExcelFile>> GetRoute(string id)
        {
            var route = await _routeService.GetRouteByIdAsync(id);

            if (route == null)
            {
                return NotFound();
            }

            return route;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoute(string id, Route route)
        {
            if (id != route.Id)
            {
                return BadRequest();
            }

            return await CustomResponseAsync(await _routeService.UpdateRouteAsync(route));
        }

        [HttpPost]
        public async Task<ActionResult<Route>> PostRoute(Route route)
        {
            return await CustomResponseAsync(await _routeService.AddRouteAsync(route));
        }

        [HttpPost("ExcelFile")]
        public async Task<ActionResult<ExcelFile>> UploadExcelFileAsync(IFormFile file)
        {
            return await CustomResponseAsync(await _routeService.UploadExcelFileAsync(file));
        }

        [HttpPost("report")]
        public async Task<ActionResult<byte[]>> PostRoute(ReportRouteRequest reportRoute)
        {
            return await CustomResponseAsync(await _routeService.ReportRoutesToDocx(reportRoute));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(string id)
        {
            var route = await GetRoute(id);
            if (route == null)
            {
                return NotFound();
            }
            await _routeService.RemoveRouteAsync(id);

            return NoContent();
        }
    }
}
