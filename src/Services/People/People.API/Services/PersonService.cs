using People.API.Repository;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Entities.Enums;
using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.Domain.Validations;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace People.API.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private readonly GatewayService _gatewayService;
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository, GatewayService gatewayService, INotifier notifier) : base(notifier)
        {
            _personRepository = personRepository;
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync() =>
            await _personRepository.GetAllAsync();

        public async Task<Person> GetPersonByIdAsync(string id) =>
            await _personRepository.FindAsync(c => c.Id == id);


        public async Task<IEnumerable<Person>> GetPersonsByIdsAsync(string ids)
        {
            var idsGuid = ids.TrimEnd(',').Split(',');

            return await _personRepository.FindAllAsync(p => idsGuid.Contains(p.Id));
        }

        public async Task<Person> AddPersonAsync(Person person)
        {
            await _gatewayService.PostLogAsync(null, person, Operation.Create);

            return !ExecuteValidation(new PersonValidation(), person) ? person : await _personRepository.AddAsync(person);
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            var personBefore = await GetPersonByIdAsync(person.Id);

            if (personBefore == null)
            {
                Notification("Not found");
                return person;
            }

            await _gatewayService.PostLogAsync(personBefore, person, Operation.Update);

            return !ExecuteValidation(new PersonValidation(), person) ? person : await _personRepository.UpdateAsync(person);
        }

        public async Task<bool> RemovePersonAsync(Person person)
        {
            await _gatewayService.PostLogAsync(null, person, Operation.Delete);

            return await _personRepository.RemoveAsync(person);
        }

        public async Task<bool> RemovePersonAsync(string id)
        {
            var person = await GetPersonByIdAsync(id);

            if (person == null)
            {
                Notification("Not found");
                return false;
            }

            await _gatewayService.PostLogAsync(null, person, Operation.Delete);

            return await _personRepository.RemoveAsync(id);
        }
    }
}
