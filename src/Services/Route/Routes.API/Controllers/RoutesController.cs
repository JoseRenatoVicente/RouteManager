﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<Route>> GetRoute(string id)
        {
            var route = await _routeService.GetRouteByIdAsync(id);

            if (route == null)
            {
                return NotFound();
            }

            return route;
        }

        [Authorize(Roles = "Rotas")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoute(string id, Route route)
        {
            if (id != route.Id)
            {
                return BadRequest();
            }

            return await CustomResponseAsync(await _routeService.UpdateRouteAsync(route));
        }

        [Authorize(Roles = "Rotas")]
        [HttpPost]
        public async Task<ActionResult<Route>> PostRoute(Route route)
        {
            return await CustomResponseAsync(await _routeService.AddRouteAsync(route));
        }

        [Authorize(Roles = "Rotas")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(string id)
        {
            var route = await _routeService.GetRouteByIdAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            await _routeService.RemoveRouteAsync(route);

            return await CustomResponseAsync();
        }


    }
}
