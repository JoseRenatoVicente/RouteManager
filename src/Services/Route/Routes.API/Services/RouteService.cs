using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Entities.Enums;
using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.Domain.Validations;
using RouteManager.WebAPI.Core.Notifications;
using Routes.API.DTO;
using Routes.API.Extensions;
using Routes.API.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.API.Services
{
    public class RouteService : BaseService, IRouteService
    {
        private readonly GatewayService _gatewayService;
        private readonly IExcelFileRepository _excelFileRepository;
        private readonly IRouteRepository _routeRepository;

        public RouteService(GatewayService gatewayService, IExcelFileRepository excelFileRepository, IRouteRepository routeRepository, INotifier notifier) : base(notifier)
        {
            _excelFileRepository = excelFileRepository;
            _gatewayService = gatewayService;
            _routeRepository = routeRepository;
        }

        public async Task<IEnumerable<ExcelFile>> GetRoutesAsync() =>
            await _excelFileRepository.GetAllAsync();

        public async Task<ExcelFile> GetRouteByIdAsync(string id) =>
            await _excelFileRepository.FindAsync(c => c.Id == id);

        public async Task<Route> AddRouteAsync(Route route)
        {
            if (!ExecuteValidation(new RouteValidation(), route)) return route;

            await _gatewayService.PostLogAsync(null, route, Operation.Create);

            return await _routeRepository.AddAsync(route);
        }

        public async Task<Route> UpdateRouteAsync(Route route)
        {
            if (!ExecuteValidation(new RouteValidation(), route)) return route;

            var routeBefore = await GetRouteByIdAsync(route.Id);

            if (routeBefore == null)
            {
                Notification("Not found");
                return route;
            }

            await _gatewayService.PostLogAsync(routeBefore, route, Operation.Update);

            return await _routeRepository.UpdateAsync(route);
        }

        public async Task RemoveRouteAsync(ExcelFile route)
        {
            await _gatewayService.PostLogAsync(null, route, Operation.Delete);

            await _excelFileRepository.RemoveAsync(route);
        }

        public async Task<bool> RemoveRouteAsync(string id)
        {
            var route = await GetRouteByIdAsync(id);

            if (route == null)
            {
                Notification("Not found");
                return false;
            }

            await _gatewayService.PostLogAsync(null, route, Operation.Delete);

            await _excelFileRepository.RemoveAsync(id);

            return true;

        }


        public async Task<ExcelFile> UploadExcelFileAsync(IFormFile file)
        {
            var table = await GetTableExcel(file);
            var excelFile = new ExcelFile();

            excelFile.FileName = file.FileName;
            excelFile.Columns = table.FirstOrDefault();

            table.RemoveAt(0);
            excelFile.Table = table;


            await _gatewayService.PostLogAsync(null, excelFile, Operation.Create);

            return await _excelFileRepository.AddAsync(excelFile);
        }

        public async Task<byte[]> ReportRoutesToDocx(ReportRouteRequest reportRoute)
        {
            if (!ExecuteValidation(new ReportRouteRequestValidation(), reportRoute)) return null;

            var excelFile = await GetRouteByIdAsync(reportRoute.ExcelFileId);

            //reportRoute.City = await _gatewayService.GetFromJsonAsync<City>("Teams/api/Cities/" + reportRoute.City.Id);

            excelFile.Table.RemoveAll(row => !CompareToIgnore(row[excelFile.Columns.IndexOf(reportRoute.NameCity)], reportRoute.City.Name) ||
                                             !CompareToIgnore(row[excelFile.Columns.IndexOf(reportRoute.NameService)], reportRoute.TypeService));

            int numberRows = excelFile.Table.Count();
            int countTeam = reportRoute.NameTeams.Count();

            excelFile.Table = excelFile.Table.OrderBy(c => c[excelFile.Columns.IndexOf(reportRoute.NameCEP)]).ToList();


            if (numberRows / 5 >= countTeam)
            {
                Notification("Equipes insuficientes para quantidade de rotas. Selecione mais equipes");
                return null;
            }

            MemoryStream memoryStream = new MemoryStream();
            XWPFDocument document = new XWPFDocument();

            XWPFRun runTitle = document.CreateParagraph().CreateRun();
            runTitle.Paragraph.Alignment = ParagraphAlignment.CENTER;
            runTitle.FontSize = 14;
            runTitle.IsBold = true;
            runTitle.SetFontFamily("Calibri", FontCharRange.Ascii);

            runTitle.AppendTextLine("ROTA TRABALHO - " + DateTime.UtcNow.ToString("dd/MM/yyyy"));

            XWPFRun textLine = document.CreateParagraph().CreateRun();
            textLine.FontSize = 9;
            textLine.SetFontFamily("Calibri", FontCharRange.Ascii);

            double divisionTeam = numberRows / reportRoute.NameTeams.Count();


            int team = 0;

            int extra = numberRows % countTeam;

            int divisao = numberRows / countTeam;

            for (int row = 0; row < numberRows; row++)
            {
                textLine.AppendTextLine($"Nome da Equipe: {reportRoute.NameTeams.ElementAt(team)}");

                if (row % countTeam == countTeam - 1 && team < countTeam - 1)
                {
                    team++;
                }
                else
                {
                    team = 0;
                }



                var route = new Route
                {
                    OS = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameOS)],
                    Base = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameBase)],
                    Service = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameService)],
                    Address = new Address
                    {
                        Street = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameStreet)],
                        Number = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameNumber)],
                        District = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameDistrict)],
                        CEP = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameCEP)],
                        Complement = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameComplement)],
                        City = new City { Name = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameCity)] }
                    },
                };

                await _routeRepository.AddAsync(route);

                if (route.Address != null) textLine.AppendTextLine($"Endereço: {route.Address}");

                if (route.OS != null) textLine.AppendText($"O.S: {route.OS} - ");
                if (route.Service != null) textLine.AppendTextLine($"TIPO O.S: {route.Service}");

                if (route.Base != null) textLine.AppendTextLine($"Base: {route.Base}");

                foreach (var item in reportRoute.ReportColumns)
                {
                    textLine.AppendTextLine($"{item}: {excelFile.Table[row][excelFile.Columns.IndexOf(item)]}");
                }
                textLine.AddBreak(BreakType.TEXTWRAPPING);
            }

            document.Write(memoryStream);
            memoryStream.Flush();

            return memoryStream.ToArray();
        }

        private bool CompareToIgnore(string stringA, string stringB)
        {
            return string.Compare(stringA, stringB, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;
        }

        public async Task<List<List<string>>> GetTableExcel(IFormFile file)
        {
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

            var table = new List<List<string>>();

            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);

                table.Add(new List<string>());

                for (int column = 0; column < 40; column++)
                {
                    if (row.GetCell(column) == null)
                        table[i].Add(null);

                    else
                        table[i].Add(row.GetCell(column).ToString());
                }
            }

            return table;
        }
    }


}
