using RouteManager.Domain.Entities;
using RouteManager.Domain.Services;
using RouteManagerMVC.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface IPersonService
    {
        Task<ResponseResult> AddPersonAsync(Person person);
        Task<Person> GetPersonByIdAsync(string id);
        Task<IEnumerable<Person>> GetPersonsAsync();
        Task RemovePersonAsync(string id);
        Task<ResponseResult> UpdatePersonAsync(Person person);
    }

    public class PersonService : IPersonService
    {
        private readonly GatewayService _gatewayService;

        public PersonService(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<Person>>("People/api/People");
        }

        public async Task<Person> GetPersonByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<Person>("People/api/People/" + id);
        }

        public async Task<ResponseResult> AddPersonAsync(Person person)
        {
            return await _gatewayService.PostAsync<ResponseResult>("People/api/People/", person);
        }

        public async Task<ResponseResult> UpdatePersonAsync(Person person)
        {
            return await _gatewayService.PutAsync<ResponseResult>("People/api/People/" + person.Id, person);
        }

        public async Task RemovePersonAsync(string id)
        {
            await _gatewayService.DeleteAsync("People/api/People/" + id);
        }

    }
}