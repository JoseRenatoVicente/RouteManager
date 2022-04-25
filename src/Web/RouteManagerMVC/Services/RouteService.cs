using Microsoft.AspNetCore.Http;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface IRouteService
    {
        Task<ReportRouteViewModel> RouteUpload(IFormFile file);
        Task<byte[]> ExportToDocx(RouteUploadRequest reportRoute);
        Task<RoleRequestViewModel> AddRouteAsync(Route route);
        Task<ReportRouteViewModel> GetRouteByIdAsync(string id);
        Task<IEnumerable<ExcelFileViewModel>> GetRoutesAsync();
        Task RemoveRouteAsync(string id);
        Task<RoleRequestViewModel> UpdateRouteAsync(Route route);
    }

    public class RouteService : BaseService, IRouteService
    {
        private readonly ICityService _cityService;
        private readonly ITeamService _teamService;
        private readonly GatewayService _gatewayService;

        public RouteService(ICityService cityService, ITeamService teamService, GatewayService gatewayService, INotifier notifier) : base(notifier)
        {
            _cityService = cityService;
            _teamService = teamService;
            _gatewayService = gatewayService;
        }

        public async Task<ReportRouteViewModel> RouteUpload(IFormFile file)
        {
            ReportRouteViewModel reportRoute = new ReportRouteViewModel();

            if (file == null)
            {
                Notification("Nenhum arquivo enviado");
                return reportRoute;
            }

            var supportedTypes = new[] { "xls", "xlsx" };
            string fileExtension = Path.GetExtension(file.FileName).Substring(1);

            if (!supportedTypes.Contains(fileExtension))
            {
                Notification("Arquivo inválido");
                return reportRoute;
            }

            reportRoute.Cities = await _cityService.GetCitysAsync();
            reportRoute.Teams = await _teamService.GetTeamsAsync();

            MultipartFormDataContent multiForm = new MultipartFormDataContent();

            var stream = new MemoryStream();
            file.CopyTo(stream);

            var bytes = new ByteArrayContent(stream.ToArray());
            multiForm.Add(bytes, "file", file.FileName);

            var excelFile = await _gatewayService.PostAsync<ExcelFileViewModel>("Routes/api/Routes/ExcelFile", multiForm);
            reportRoute.ExcelFile = excelFile;
            reportRoute.UploadRequest.ExcelFileId = excelFile.Id;

            return reportRoute;
        }

        public async Task<byte[]> ExportToDocx(RouteUploadRequest reportRoute)
        {
            var response = await _gatewayService.PostAsync("Routes/api/Routes/report", reportRoute);

            return response.IsSuccessStatusCode ? Convert.FromBase64String((await response.Content.ReadAsStringAsync()).Trim('"')) : null;
        }

        public async Task<IEnumerable<ExcelFileViewModel>> GetRoutesAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<ExcelFileViewModel>>("Routes/api/Routes");
        }

        public async Task<ReportRouteViewModel> GetRouteByIdAsync(string id)
        {
            ReportRouteViewModel reportRoute = new ReportRouteViewModel();

            reportRoute.Cities = await _cityService.GetCitysAsync();
            reportRoute.Teams = await _teamService.GetTeamsAsync();

            var excelFile = await _gatewayService.GetFromJsonAsync<ExcelFileViewModel>("Routes/api/Routes/" + id);
            reportRoute.ExcelFile = excelFile;
            reportRoute.UploadRequest.ExcelFileId = excelFile.Id;

            return reportRoute;
        }

        public async Task<RoleRequestViewModel> AddRouteAsync(Route route)
        {
            return await _gatewayService.PostAsync<RoleRequestViewModel>("Routes/api/Routes/", route);
        }

        public async Task<RoleRequestViewModel> UpdateRouteAsync(Route route)
        {
            return await _gatewayService.PutAsync<RoleRequestViewModel>("Routes/api/Routes/" + route.Id, route);
        }

        public async Task RemoveRouteAsync(string id)
        {
            await _gatewayService.DeleteAsync("Routes/api/Routes/" + id);
        }

    }
}