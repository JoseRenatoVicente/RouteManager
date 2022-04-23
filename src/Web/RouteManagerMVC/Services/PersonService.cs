using RouteManager.Domain.Entities;
using RouteManager.Domain.Services;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RouteManagerMVC.Services
{
    public interface IPersonService
    {
        Task<ResponseResult> AddPersonAsync(PersonViewModel person);
        Task<PersonViewModel> GetPersonByIdAsync(string id);
        Task<IEnumerable<PersonViewModel>> GetPersonsByIdsAsync(IEnumerable<string> ids);
        Task<IEnumerable<PersonViewModel>> GetPersonsAsync();
        Task RemovePersonAsync(string id);
        Task<ResponseResult> UpdatePersonAsync(PersonViewModel person);
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
            return await _gatewayService.GetFromJsonAsync<IEnumerable<PersonViewModel>>("People/api/People");
        }

        public async Task<PersonViewModel> GetPersonByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<PersonViewModel>("People/api/People/" + id);
        }

        public async Task<IEnumerable<PersonViewModel>> GetPersonsByIdsAsync(IEnumerable<string> ids)
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<PersonViewModel>>("People/api/People/list/" + string.Concat(ids.Select(c => c + ",")));
        }

        public async Task<ResponseResult> AddPersonAsync(PersonViewModel person)
        {
            return await _gatewayService.PostAsync<ResponseResult>("People/api/People/", person);
        }

        public async Task<ResponseResult> UpdatePersonAsync(PersonViewModel person)
        {
            return await _gatewayService.PutAsync<ResponseResult>("People/api/People/" + person.Id, person);
        }

        public async Task RemovePersonAsync(string id)
        {
            await _gatewayService.DeleteAsync("People/api/People/" + id);
        }

    }
}