using RouteManager.Domain.Services;
using RouteManagerMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface IRouteService
    {
        Task<RouteRequestViewModel> AddRouteAsync(RouteRequestViewModel route);
        Task<RouteRequestViewModel> GetRouteByIdAsync(string id);
        Task<IEnumerable<RouteRequestViewModel>> GetRoutesAsync();
        Task RemoveRouteAsync(string id);
        Task<RouteRequestViewModel> UpdateRouteAsync(RouteRequestViewModel route);
    }

    public class RouteService : IRouteService
    {
        private readonly GatewayService _gatewayService;

        public RouteService(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }


        public async Task<IEnumerable<RouteRequestViewModel>> GetRoutesAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<RouteRequestViewModel>>("Routes/api/Routes/");
        }
        public async Task<RouteRequestViewModel> GetRouteByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<RouteRequestViewModel>("Routes/api/Routes/" + id);
        }

        public async Task<RouteRequestViewModel> AddRouteAsync(RouteRequestViewModel route)
        {
            return await _gatewayService.PostAsync<RouteRequestViewModel>("Routes/api/Routes/", route);
        }

        public async Task<RouteRequestViewModel> UpdateRouteAsync(RouteRequestViewModel route)
        {
            return await _gatewayService.PutAsync<RouteRequestViewModel>("Routes/api/Routes/" + route.Id, route);
        }

        public async Task RemoveRouteAsync(string id)
        {
            await _gatewayService.DeleteAsync("Routes/api/Routes/" + id);
        }
    }
}
