using Microsoft.AspNetCore.Http;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Services;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface IRouteService
    {
        Task<ReportRouteViewModel> RouteUpload(IFormFile file);
        Task<byte[]> ExportToDocx(RouteUploadRequest reportRoute);
        Task<ResponseResult> AddRouteAsync(Route route);
        Task<ReportRouteViewModel> GetRouteByIdAsync(string id);
        Task<IEnumerable<ExcelFileViewModel>> GetRoutesAsync();
        Task RemoveRouteAsync(string id);
        Task<ResponseResult> UpdateRouteAsync(Route route);
    }

    public class RouteService : IRouteService
    {
        private readonly ICityService _cityService;
        private readonly ITeamService _teamService;
        private readonly GatewayService _gatewayService;

        public RouteService(ICityService cityService, ITeamService teamService, GatewayService gatewayService)
        {
            _cityService = cityService;
            _teamService = teamService;
            _gatewayService = gatewayService;
        }


        public async Task<ReportRouteViewModel> RouteUpload(IFormFile file)
        {
            ReportRouteViewModel reportRoute = new ReportRouteViewModel();

            reportRoute.Cities = await _cityService.GetCitysAsync();
            reportRoute.Teams = await _teamService.GetTeamsAsync();

            MultipartFormDataContent multiForm = new MultipartFormDataContent();

            var stream = new MemoryStream();
            file.CopyTo(stream);

            var bytes = new ByteArrayContent(stream.ToArray());
            multiForm.Add(bytes, "file", file.FileName);

            var excelFile = await _gatewayService.PostAsync<ExcelFileViewModel>("Routes/api/Routes/ExcelFile", multiForm);
            reportRoute.ExcelFile = excelFile;

            return reportRoute;

        }

        public async Task<byte[]> ExportToDocx(RouteUploadRequest reportRoute)
        {
            var response = await _gatewayService.PostAsync("Routes/api/Routes/report", reportRoute);

            return Convert.FromBase64String((await response.Content.ReadAsStringAsync()).Trim('"'));
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

        public async Task<ResponseResult> AddRouteAsync(Route route)
        {
            return await _gatewayService.PostAsync<ResponseResult>("Routes/api/Routes/", route);
        }

        public async Task<ResponseResult> UpdateRouteAsync(Route route)
        {
            return await _gatewayService.PutAsync<ResponseResult>("Routes/api/Routes/" + route.Id, route);
        }

        public async Task RemoveRouteAsync(string id)
        {
            await _gatewayService.DeleteAsync("Routes/api/Routes/" + id);
        }

    }
}