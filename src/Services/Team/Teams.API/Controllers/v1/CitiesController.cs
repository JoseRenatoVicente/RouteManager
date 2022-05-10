using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Threading.Tasks;
using Teams.API.Services;
using Teams.Domain.Commands.v1.Cities.Create;
using Teams.Domain.Commands.v1.Cities.Delete;
using Teams.Domain.Commands.v1.Cities.Update;

namespace Teams.API.Controllers.v1;

[Route("api/v1/[controller]")]
public class CitiesController : BaseController
{
    private readonly ICityService _citysService;

    public CitiesController(ICityService citysService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _citysService = citysService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCity()
    {
        return Ok(await _citysService.GetCitysAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCity(string id)
    {
        var city = await _citysService.GetCityByIdAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        return Ok(city);
    }

    [Authorize(Roles = "Cidades")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCity(string id, UpdateCityCommand updateCity)
    {
        return await CustomResponseAsync(updateCity);
    }

    [Authorize(Roles = "Cidades")]
    [HttpPost]
    public async Task<IActionResult> PostCity(CreateCityCommand createCity)
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