using RouteManager.Domain.Services;
using RouteManagerMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface IPersonService
    {
        Task<PersonViewModel> AddPersonAsync(PersonViewModel person);
        Task<PersonViewModel> GetPersonByIdAsync(string id);
        Task<IEnumerable<PersonViewModel>> GetPersonsByIdsAsync(IEnumerable<string> ids);
        Task<IEnumerable<PersonViewModel>> GetPersonsAsync();
        Task RemovePersonAsync(string id);
        Task<PersonViewModel> UpdatePersonAsync(PersonViewModel person);
    }

    public class PersonService : IPersonService
    {
        private readonly GatewayService _gatewayService;

        public PersonService(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<PersonViewModel>> GetPersonsAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<PersonViewModel>>("Teams/api/People");
        }

        public async Task<PersonViewModel> GetPersonByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<PersonViewModel>("Teams/api/People/" + id);
        }

        public async Task<IEnumerable<PersonViewModel>> GetPersonsByIdsAsync(IEnumerable<string> ids)
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<PersonViewModel>>("Teams/api/People/list/" + string.Concat(ids.Select(c => c + ",")));
        }

        public async Task<PersonViewModel> AddPersonAsync(PersonViewModel person)
        {
            return await _gatewayService.PostAsync<PersonViewModel>("Teams/api/People/", person);
        }

        public async Task<PersonViewModel> UpdatePersonAsync(PersonViewModel person)
        {
            return await _gatewayService.PutAsync<PersonViewModel>("Teams/api/People/" + person.Id, person);
        }

        public async Task RemovePersonAsync(string id)
        {
            await _gatewayService.DeleteAsync("Teams/api/People/" + id);
        }

    }
}