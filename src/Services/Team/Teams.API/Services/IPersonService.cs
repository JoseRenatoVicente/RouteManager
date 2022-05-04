using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.Domain.Entities.v1;

namespace Teams.API.Services
{
    public interface IPersonService
    {
        Task<Person> AddPersonAsync(Person person);
        Task<Person> GetPersonByIdAsync(string id);
        Task<IEnumerable<Person>> GetPersonsByIdsAsync(string ids);
        Task<IEnumerable<Person>> GetPersonsAsync(bool available = false);
        Task<bool> RemovePersonAsync(Person person);
        Task<bool> RemovePersonAsync(string id);
        Task<Person> UpdatePersonAsync(Person person);
    }
}