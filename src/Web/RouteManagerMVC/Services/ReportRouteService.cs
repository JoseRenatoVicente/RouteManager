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
    public interface IReportRouteService
    {
        Task<ReportRouteViewModel> RouteUpload(IFormFile file);
        Task<byte[]> ExportToDocx(RouteUploadRequest reportRoute);
        Task<ReportRouteViewModel> GetRouteByIdAsync(string id);
        Task<IEnumerable<ExcelFileViewModel>> GetRoutesAsync();
        Task RemoveRouteAsync(string id);
    }

    public class ReportRouteService : BaseService, IReportRouteService
    {
        private readonly ICityService _cityService;
        private readonly ITeamService _teamService;
        private readonly GatewayService _gatewayService;

        public ReportRouteService(ICityService cityService, ITeamService teamService, GatewayService gatewayService, INotifier notifier) : base(notifier)
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
                Notification("Arquivo inválido. Apenas formatos xls e xlsx");
                return reportRoute;
            }

            reportRoute.Cities = await _cityService.GetCitysAsync();
            reportRoute.Teams = await _teamService.GetTeamsAsync();

            MultipartFormDataContent multiForm = new MultipartFormDataContent();

            var stream = new MemoryStream();
            file.CopyTo(stream);

            var bytes = new ByteArrayContent(stream.ToArray());
            multiForm.Add(bytes, "file", file.FileName);

            var excelFile = await _gatewayService.PostAsync<ExcelFileViewModel>("Routes/api/ExcelFile", multiForm);
            reportRoute.ExcelFile = excelFile;
            reportRoute.UploadRequest.ExcelFileId = excelFile.Id;

            return reportRoute;
        }

        public async Task<byte[]> ExportToDocx(RouteUploadRequest reportRoute)
        {
            var response = await _gatewayService.PostAsync("Routes/api/ExcelFile/report", reportRoute);

            return response.IsSuccessStatusCode ? Convert.FromBase64String((await response.Content.ReadAsStringAsync()).Trim('"')) : null;
        }

        public async Task<IEnumerable<ExcelFileViewModel>> GetRoutesAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<ExcelFileViewModel>>("Routes/api/ExcelFile");
        }

        public async Task<ReportRouteViewModel> GetRouteByIdAsync(string id)
        {
            ReportRouteViewModel reportRoute = new ReportRouteViewModel();

            reportRoute.Cities = await _cityService.GetCitysAsync();
            reportRoute.Teams = await _teamService.GetTeamsAsync();

            var excelFile = await _gatewayService.GetFromJsonAsync<ExcelFileViewModel>("Routes/api/ExcelFile/" + id);
            reportRoute.ExcelFile = excelFile;
            reportRoute.UploadRequest.ExcelFileId = excelFile.Id;

            return reportRoute;
        }

        public async Task RemoveRouteAsync(string id)
        {
            await _gatewayService.DeleteAsync("Routes/api/ExcelFile/" + id);
        }

    }
}