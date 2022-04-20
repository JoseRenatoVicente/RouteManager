using Microsoft.AspNetCore.Http;
using RouteManager.Domain.Entities;
using Routes.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Routes.API.Services
{
    public interface IRouteService
    {
        Task<Route> AddRouteAsync(Route route);
        Task<Route> GetRouteByIdAsync(string id);
        Task<IEnumerable<Route>> GetRoutesAsync();
        Task<List<List<string>>> GetTableExcel(IFormFile file);
        Task<RouteExcel> HeaderExcel(IFormFile file);
        Task RemoveRouteAsync(Route route);
        Task RemoveRouteAsync(string id);
        Task<byte[]> ReportRoutesToDocx(IFormFile file, ReportRouteRequest reportRota);
        Task<Route> UpdateRouteAsync(Route route);
    }
}