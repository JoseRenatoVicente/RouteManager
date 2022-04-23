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
        Task<ExcelFile> GetRouteByIdAsync(string id);
        Task<IEnumerable<ExcelFile>> GetRoutesAsync();
        Task<List<List<string>>> GetTableExcel(IFormFile file);
        Task RemoveRouteAsync(ExcelFile route);
        Task<bool> RemoveRouteAsync(string id);
        Task<ExcelFile> UploadExcelFileAsync(IFormFile file);
        Task<byte[]> ReportRoutesToDocx(ReportRouteRequest reportRoute);
        Task<Route> UpdateRouteAsync(Route route);
    }
}