using RouteManager.Domain.Entities;
using RouteManager.Domain.Entities.Enums;
using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.Domain.Validations;
using RouteManager.WebAPI.Core.Notifications;
using Routes.API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Routes.API.Services
{
    public interface IRouteService
    {
        Task<Route> AddRouteAsync(Route route);
        Task<Route> GetRouteByIdAsync(string id);
        Task<IEnumerable<Route>> GetRoutesAsync();
        Task RemoveRouteAsync(Route route);
        Task<bool> RemoveRouteAsync(string id);
        Task<Route> UpdateRouteAsync(Route route);
    }

    public class RouteService : BaseService, IRouteService
    {
        private readonly GatewayService _gatewayService;
        private readonly IRouteRepository _routeRepository;

        public RouteService(GatewayService gatewayService, IRouteRepository routeRepository, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
            _routeRepository = routeRepository;
        }

        public async Task<IEnumerable<Route>> GetRoutesAsync()
        {
           return await _routeRepository.GetAllAsync();
        }

        public async Task<Route> GetRouteByIdAsync(string id) =>
            await _routeRepository.FindAsync(c => c.Id == id);

        public async Task<Route> AddRouteAsync(Route route)
        {
            await _gatewayService.PostLogAsync(null, route, Operation.Create);

            return !ExecuteValidation(new RouteValidation(), route) ? route : await _routeRepository.AddAsync(route);
        }

        public async Task<Route> UpdateRouteAsync(Route route)
        {
            var routeBefore = await GetRouteByIdAsync(route.Id);
            if (routeBefore == null)
            {
                Notification("Rota não encontrada");
                return route;
            }

            await _gatewayService.PostLogAsync(routeBefore, route, Operation.Update);

            return !ExecuteValidation(new RouteValidation(), route) ? route : await _routeRepository.UpdateAsync(route);
        }

        public async Task RemoveRouteAsync(Route route)
        {
            await _gatewayService.PostLogAsync(null, route, Operation.Delete);
            await _routeRepository.RemoveAsync(route);
        }

        public async Task<bool> RemoveRouteAsync(string id)
        {
            var route = await GetRouteByIdAsync(id);
            if (route == null)
            {
                Notification("Rota não encontrada");
                return false;
            }

            await _gatewayService.PostLogAsync(null, route, Operation.Delete);
            return await _routeRepository.RemoveAsync(id);
        }
    }


}
