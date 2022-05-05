using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using RouteManagerMVC.Services;
using System.Threading.Tasks;

namespace RouteManagerMVC.Controllers;

public class CitiesController : MvcBaseController
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService, INotifier notifier) : base(notifier)
    {
        _cityService = cityService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _cityService.GetCitysAsync());
    }

    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var city = await _cityService.GetCityByIdAsync(id);
        if (city == null)
        {
            return NotFound();
        }

        return View(city);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,State,Id")] CityViewModel city)
    {
        return await CustomResponseAsync(await _cityService.AddCityAsync(city));
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var city = await _cityService.GetCityByIdAsync(id);
        if (city == null)
        {
            return NotFound();
        }
        return View(city);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Name,State,Id")] CityViewModel city)
    {
        if (id != city.Id)
        {
            return NotFound();
        }
        return await CustomResponseAsync(await _cityService.UpdateCityAsync(city));
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var city = await _cityService.GetCityByIdAsync(id);
        if (city == null)
        {
            return NotFound();
        }
        return View(city);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var city = await _cityService.GetCityByIdAsync(id);

        await _cityService.RemoveCityAsync(id);

        return await CustomResponseAsync(city);
    }
}