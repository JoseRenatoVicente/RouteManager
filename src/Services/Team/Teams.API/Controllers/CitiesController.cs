using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.API.Services;
using Teams.Domain.Commands.Cities.Create;
using Teams.Domain.Commands.Cities.Delete;
using Teams.Domain.Commands.Cities.Update;
using Teams.Domain.Entities.v1;

namespace Teams.API.Controllers;

[Route("api/v1/[controller]")]
public class CitiesController : BaseController
{
    private readonly ICityService _citysService;

    public CitiesController(ICityService citysService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _citysService = citysService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetCity()
    {
        return Ok(await _citysService.GetCitysAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetCity(string id)
    {
        var city = await _citysService.GetCityByIdAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        return city;
    }

    [Authorize(Roles = "Cidades")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCity(string id, UpdateCityCommand updateCity)
    {
        return await CustomResponseAsync(updateCity);
    }

    [Authorize(Roles = "Cidades")]
    [HttpPost]
    public async Task<ActionResult<City>> PostCity(CreateCityCommand createCity)
    {
        return await CustomResponseAsync(createCity);
    }

    [Authorize(Roles = "Cidades")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity([FromRoute] DeleteCityCommand deleteCity)
    {
        return await CustomResponseAsync(deleteCity);
    }
}