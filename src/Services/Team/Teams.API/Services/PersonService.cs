using MongoDB.Bson;
using MongoDB.Driver;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Entities.Enums;
using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.Domain.Validations;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.API.Repository;

namespace Teams.API.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly GatewayService _gatewayService;

        public PersonService(IPersonRepository personRepository, ITeamRepository teamRepository, GatewayService gatewayService, INotifier notifier) : base(notifier)
        {
            _personRepository = personRepository;
            _teamRepository = teamRepository;
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<Person> GetPersonByIdAsync(string id) =>
            await _personRepository.FindAsync(c => c.Id == id);


        public async Task<IEnumerable<Person>> GetPersonsByIdsAsync(string ids)
        {
            var idsGuid = ids.TrimEnd(',').Split(',');

            return await _personRepository.FindAllAsync(p => idsGuid.Contains(p.Id));
        }

        public async Task<Person> AddPersonAsync(Person person)
        {
            person.Id = ObjectId.GenerateNewId().ToString();
            await _gatewayService.PostLogAsync(null, person, Operation.Create);

            return !ExecuteValidation(new PersonValidation(), person) ? person : await _personRepository.AddAsync(person);
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            var personBefore = await GetPersonByIdAsync(person.Id);

            if (personBefore == null)
            {
                Notification("Pessoa não encontrada");
                return person;
            }

            var team = await _teamRepository.FindAsync(c => c.People.Any(c => c.Id == person.Id) );
            if (team != null)
            {
                team.People = team.People.Select(c =>
                {
                    if (c.Id == person.Id)
                    {
                        c = person;
                    }
                    return c;
                });
            }

            await _teamRepository.UpdateAsync(team);

            await _gatewayService.PostLogAsync(personBefore, person, Operation.Update);

            return !ExecuteValidation(new PersonValidation(), person) ? person : await _personRepository.UpdateAsync(person);
        }


        public async Task<bool> RemovePersonAsync(string id)
        {
            var person = await GetPersonByIdAsync(id);

            if (person == null)
            {
                Notification("Pessoa não encontrada");
                return false;
            }

            return await RemovePersonAsync(person);
        }
        public async Task<bool> RemovePersonAsync(Person person)
        {
            if (await _teamRepository.FindAsync(c => c.People.Any(c => c.Id == person.Id)) != null)
            {
                Notification("Essa Pessoa possui vinculo com uma equipe, exclua primeiro a equipe para excluir a pessoa");
                return false;
            }

            await _gatewayService.PostLogAsync(null, person, Operation.Delete);

            return await _personRepository.RemoveAsync(person);
        }
    }
}
