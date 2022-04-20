using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Services;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface IRouteService
    {
        Task<ReportRouteViewModel> RouteUpload(IFormFile file);
        Task<ResponseResult> AddRouteAsync(Route route);
        Task<Route> GetRouteByIdAsync(string id);
        Task<IEnumerable<Route>> GetRoutesAsync();
        Task RemoveRouteAsync(string id);
        Task<ResponseResult> UpdateRouteAsync(Route route);
    }

    public class RouteService : IRouteService
    {
        private readonly GatewayService _gatewayService;

        public RouteService(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }


        public async Task<ReportRouteViewModel> RouteUpload(IFormFile file)
        {
            ReportRouteViewModel reportRoute = new ReportRouteViewModel();

            string sFileExtension = Path.GetExtension(file.FileName).ToLower();
            ISheet sheet;

            if (sFileExtension == ".xls")
            {
                HSSFWorkbook hssfwb = new HSSFWorkbook(file.OpenReadStream()); //This will read the Excel 97-2000 formats  
                sheet = hssfwb.GetSheetAt(0);
            }
            else
            {
                XSSFWorkbook hssfwb = new XSSFWorkbook(file.OpenReadStream()); //This will read 2007 Excel format  
                sheet = hssfwb.GetSheetAt(0);
            }

            reportRoute.TableHTML.Append("<table class='table table-bordered'><tr>");

            reportRoute.Columns = sheet.GetRow(0).Cells.Select(c => c.StringCellValue);


            for (int j = 0; j < reportRoute.Columns.Count(); j++)
            {
                reportRoute.TableHTML.Append("<th>" + reportRoute.Columns.ElementAt(j) + "</th>");
            }

            reportRoute.Table = new List<List<string>>();

            reportRoute.TableHTML.Append("</tr>");
            reportRoute.TableHTML.AppendLine("<tr>");
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                reportRoute.Table.Add(new List<string>());

                if (row == null) continue;
                for (int column = row.FirstCellNum; column < reportRoute.Columns.Count(); column++)
                {
                    if (row.GetCell(column) == null)
                    {
                        if (i == 3) reportRoute.TableHTML.Append("<td></td>");
                        reportRoute.Table[i].Add("");
                    }
                    else
                    {
                        if (i == 3) reportRoute.TableHTML.Append("<td>" + row.GetCell(column).ToString() + "</td>");
                        reportRoute.Table[i].Add(row.GetCell(column).ToString());
                    }




                }
                reportRoute.TableHTML.AppendLine("</tr>");
            }
            reportRoute.TableHTML.Append("</table>");

            return reportRoute;
        }

        public async Task<IEnumerable<Route>> GetRoutesAsync()
        {
            return await _gatewayService.GetFromJsonAsync<IEnumerable<Route>>("Routes/api/Routes");
        }

        public async Task<Route> GetRouteByIdAsync(string id)
        {
            return await _gatewayService.GetFromJsonAsync<Route>("Routes/api/Routes/" + id);
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