using People.API.Repository;
using RouteManager.Domain.Entities;
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
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository, INotifier notifier) : base(notifier)
        {
            _personRepository = personRepository;
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
            if (!ExecuteValidation(new PersonValidation(), person)) return person;

            return await _personRepository.AddAsync(person);
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            if (!ExecuteValidation(new PersonValidation(), person)) return person;

            return await _personRepository.UpdateAsync(person);
        }

        public async Task RemovePersonAsync(Person person) =>
            await _personRepository.RemoveAsync(person);

        public async Task RemovePersonAsync(string id) =>
            await _personRepository.RemoveAsync(id);
    }
}
