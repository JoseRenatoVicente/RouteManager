using RouteManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace People.API.Services
{
    public interface IPersonService
    {
        Task<Person> AddPersonAsync(Person person);
        Task<Person> GetPersonByIdAsync(string id);
        Task<IEnumerable<Person>> GetPersonsByIdsAsync(string ids);
        Task<IEnumerable<Person>> GetPersonsAsync();
        Task RemovePersonAsync(Person person);
        Task RemovePersonAsync(string id);
        Task<Person> UpdatePersonAsync(Person person);
    }
}