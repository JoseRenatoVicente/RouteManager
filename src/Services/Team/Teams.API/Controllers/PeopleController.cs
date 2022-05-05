using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.API.Services;
using Teams.Domain.Commands.People.Create;
using Teams.Domain.Commands.People.Delete;
using Teams.Domain.Commands.People.Update;
using Teams.Domain.Entities.v1;

namespace Teams.API.Controllers;

[Route("api/v1/[controller]")]
public class PeopleController : BaseController
{
    private readonly IPersonService _personsService;

    public PeopleController(IPersonService personsService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _personsService = personsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetPerson(bool available = false)
    {
        return Ok(await _personsService.GetPersonsAsync(available));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPerson(string id)
    {
        var person = await _personsService.GetPersonByIdAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return person;
    }

    [HttpGet("list/{ids}")]
    public async Task<ActionResult<Person>> GetPersons(string ids)
    {
        return Ok(await _personsService.GetPersonsByIdsAsync(ids));
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
    public async Task<ActionResult<Person>> PostPerson(CreatePersonCommand createPerson)
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