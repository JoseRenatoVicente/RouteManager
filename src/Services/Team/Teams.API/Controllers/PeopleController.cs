using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Entities;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.API.Services;

namespace Teams.API.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : BaseController
    {
        private readonly IPersonService _personsService;

        public PeopleController(INotifier notifier, IPersonService personsService) : base(notifier)
        {
            _personsService = personsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return Ok(await _personsService.GetPersonsAsync());
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
        public async Task<IActionResult> PutPerson(string id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            return await CustomResponseAsync(await _personsService.UpdatePersonAsync(person));
        }

        [Authorize(Roles = "Pessoas")]
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            return await CustomResponseAsync(await _personsService.AddPersonAsync(person));
        }

        [Authorize(Roles = "Pessoas")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(string id)
        {
            var person = await GetPerson(id);
            if (person == null)
            {
                return NotFound();
            }
            await _personsService.RemovePersonAsync(id);

            return NoContent();
        }
    }
}
