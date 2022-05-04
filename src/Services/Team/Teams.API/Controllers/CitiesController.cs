using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.API.Services;
using Teams.Domain.Entities.v1;

namespace Teams.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CitiesController : BaseController
    {
        private readonly ICityService _citysService;

        public CitiesController(INotifier notifier, ICityService citysService) : base(notifier)
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
        public async Task<IActionResult> PutCity(string id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            return await CustomResponseAsync(await _citysService.UpdateCityAsync(city));
        }

        [Authorize(Roles = "Cidades")]
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            return await CustomResponseAsync(await _citysService.AddCityAsync(city));
        }

        [Authorize(Roles = "Cidades")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(string id)
        {
            var city = await _citysService.GetCityByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return await CustomResponseAsync(await _citysService.RemoveCityAsync(city));
        }
    }
}
