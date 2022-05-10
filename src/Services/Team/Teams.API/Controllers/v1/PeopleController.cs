using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Threading.Tasks;
using Teams.API.Services;
using Teams.Domain.Commands.v1.People.Create;
using Teams.Domain.Commands.v1.People.Delete;
using Teams.Domain.Commands.v1.People.Update;

namespace Teams.API.Controllers.v1;

[Route("api/v1/[controller]")]
public class PeopleController : BaseController
{
    private readonly IPersonService _personsService;

    public PeopleController(IPersonService personsService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _personsService = personsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPerson(bool available = false)
    {
        return Ok(await _personsService.GetPersonsAsync(available));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(string id)
    {
        var person = await _personsService.GetPersonByIdAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    [HttpGet("list/{ids}")]
    public async Task<IActionResult> GetPersons(string ids)
    {
        return await CustomResponseAsync(await _personsService.GetPersonsByIdsAsync(ids));
    }

    [Authorize(Roles = "Pessoas")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(string id, UpdatePersonCommand updatePerson)
    {
        if (id != updatePerson.Id)
        {
            return BadRequest();
        }

        return await CustomResponseAsync(updatePerson);
    }

    [Authorize(Roles = "Pessoas")]
    [HttpPost]
    public async Task<IActionResult> PostPerson(CreatePersonCommand createPerson)
    {
        return await CustomResponseAsync(createPerson);
    }

    [Authorize(Roles = "Pessoas")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson([FromRoute] DeletePersonCommand deletePerson)
    {
        return await CustomResponseAsync(deletePerson);
    }
}