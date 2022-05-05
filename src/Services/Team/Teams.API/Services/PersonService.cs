using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;

namespace Teams.API.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<IEnumerable<Person>> GetPersonsAsync(bool available = false)
    {
        if (available)
        {
            return await _personRepository.FindAllAsync(c => c.Active == true);
        }
        return await _personRepository.GetAllAsync();
    }

    public async Task<Person> GetPersonByIdAsync(string id) =>
        await _personRepository.FindAsync(c => c.Id == id);


    public async Task<IEnumerable<Person>> GetPersonsByIdsAsync(string ids)
    {
        var idsGuid = ids.TrimEnd(',').Split(',');

        return await _personRepository.FindAllAsync(p => idsGuid.Contains(p.Id));
    }
}