using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using RouteManagerMVC.Services;
using System.Threading.Tasks;

namespace RouteManagerMVC.Controllers
{
    public class PeopleController : MVCBaseController
    {
        private readonly IPersonService _personService;

        public PeopleController(IPersonService personService, INotifier notifier) : base(notifier)
        {
            _personService = personService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _personService.GetPersonsAsync(false));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _personService.GetPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] PersonViewModel person)
        {
            return await CustomResponseAsync(await _personService.AddPersonAsync(person));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _personService.GetPersonByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Id")] PersonViewModel person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            return await CustomResponseAsync(await _personService.UpdatePersonAsync(person));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _personService.GetPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await _personService.GetPersonByIdAsync(id);

            await _personService.RemovePersonAsync(person.Id);

            return await CustomResponseAsync(person);
        }

    }
}
