using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.Domain.Validations;
using RouteManager.WebAPI.Core.Notifications;
using Routes.API.DTO;
using Routes.API.Repository;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routes.API.Services
{
    public class RouteService : BaseService, IRouteService
    {
        private readonly GatewayService _gatewayService;
        private readonly IRouteRepository _routeRepository;

        public RouteService(IRouteRepository routeRepository, GatewayService gatewayService, INotifier notifier) : base(notifier)
        {
            _routeRepository = routeRepository;
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<Route>> GetRoutesAsync() =>
            await _routeRepository.GetAllAsync();

        public async Task<Route> GetRouteByIdAsync(string id) =>
            await _routeRepository.FindAsync(c => c.Id == id);

        public async Task<Route> AddRouteAsync(Route route)
        {
            if (!ExecuteValidation(new RouteValidation(), route)) return route;

            return await _routeRepository.AddAsync(route);
        }

        public async Task<Route> UpdateRouteAsync(Route route)
        {
            if (!ExecuteValidation(new RouteValidation(), route)) return route;

            return await _routeRepository.UpdateAsync(route);
        }

        public async Task RemoveRouteAsync(Route route) =>
            await _routeRepository.RemoveAsync(route);

        public async Task RemoveRouteAsync(string id) =>
            await _routeRepository.RemoveAsync(id);

        public async Task<byte[]> ReportRoutesToDocx(IFormFile file, ReportRouteRequest reportRota)
        {
            var table = await GetTableExcel(file);

            List<string> columns = table[0].ToList();
            int numberRows = columns.Count();
            IEnumerable<Team> teams = await _gatewayService.GetFromJsonAsync<IEnumerable<Team>>("Team/api/Teams/");
            StringBuilder stringBuilder = new StringBuilder();

            for (int row = 1; row < numberRows; row++)
            {
                var rota = new Route
                {
                    OS = table[columns.IndexOf(reportRota.NameOS)][row],
                    Base = table[columns.IndexOf(reportRota.NameBase)][row],
                    Service = table[columns.IndexOf(reportRota.NameService)][row],
                    Address = new Address
                    {
                        Street = table[columns.IndexOf(reportRota.NameStreet)][row],
                        Number = table[columns.IndexOf(reportRota.NameNumber)][row],
                        District = table[columns.IndexOf(reportRota.NameDistrict)][row],
                        CEP = table[columns.IndexOf(reportRota.NameCEP)][row],
                        Complement = table[columns.IndexOf(reportRota.NameComplement)][row],
                        City = new City { Name = table[columns.IndexOf(reportRota.NameCity)][row] }
                    },
                };

                await _routeRepository.AddAsync(rota);

                if (rota.OS != null) stringBuilder.Append($"{reportRota.NameOS}: {rota.OS}");
                if (rota.Base != null) stringBuilder.Append($"{reportRota.NameBase}: {rota.Base}");
                if (rota.Service != null) stringBuilder.Append($"{reportRota.NameService}: {rota.Service}");

                if (rota.Address.Street != null) stringBuilder.Append($"{reportRota.NameStreet}: {rota.Address.Street}");
                if (rota.Address.Number != null) stringBuilder.Append($"{reportRota.NameNumber}: {rota.Address.Number}");
                if (rota.Address.District != null) stringBuilder.Append($"{reportRota.NameDistrict}: {rota.Address.District}");
                if (rota.Address.CEP != null) stringBuilder.Append($"{reportRota.NameCEP}: {rota.Address.CEP}");
                if (rota.Address.Complement != null) stringBuilder.Append($"{reportRota.NameComplement}: {rota.Address.Complement}");
            }

            MemoryStream ms = new MemoryStream();
            XWPFDocument doc = new XWPFDocument();

            XWPFParagraph p1 = doc.CreateParagraph();
            p1.Alignment = ParagraphAlignment.CENTER;
            XWPFRun runTitle = p1.CreateRun();
            runTitle.IsBold = true;
            runTitle.SetText("Equipe");
            runTitle.FontSize = 18;

            XWPFParagraph p2 = doc.CreateParagraph();
            p2.Alignment = ParagraphAlignment.CENTER;
            XWPFRun runTitle2 = p2.CreateRun();
            runTitle2.FontSize = 12;
            runTitle2.SetText(stringBuilder.ToString());

            /* 
             //header
             tableContent.GetRow(0).GetCell(0).SetParagraph(SetCellText(doc, tableContent, reportRota.NameOS));
             tableContent.GetRow(0).GetCell(1).SetParagraph(SetCellText(doc, tableContent, reportRota.NameBase));
             tableContent.GetRow(0).GetCell(2).SetParagraph(SetCellText(doc, tableContent, reportRota.NameServico));
             tableContent.GetRow(0).GetCell(3).SetParagraph(SetCellText(doc, tableContent, reportRota.NameRua));
             tableContent.GetRow(0).GetCell(4).SetParagraph(SetCellText(doc, tableContent, reportRota.NameNumero));
             tableContent.GetRow(0).GetCell(5).SetParagraph(SetCellText(doc, tableContent, reportRota.NameBairro));
             tableContent.GetRow(0).GetCell(6).SetParagraph(SetCellText(doc, tableContent, reportRota.NameCidade));
             tableContent.GetRow(0).GetCell(7).SetParagraph(SetCellText(doc, tableContent, reportRota.NameComplemento));
             tableContent.GetRow(0).GetCell(8).SetParagraph(SetCellText(doc, tableContent, reportRota.NameCEP));
             tableContent.GetRow(0).GetCell(9).SetParagraph(SetCellText(doc, tableContent, "Equipe"));

             for (int row = 0; row < numberRows; row++)
             {
                 tableContent.GetRow(row + 1).GetCell(1).SetParagraph(SetCellText(doc, tableContent, reportRota.Table[columns.IndexOf(reportRota.NameOS)][row]));
                 tableContent.GetRow(row + 1).GetCell(0).SetParagraph(SetCellText(doc, tableContent, reportRota.Table[columns.IndexOf(reportRota.NameBase)][row]));
                 tableContent.GetRow(row + 1).GetCell(1).SetParagraph(SetCellText(doc, tableContent, reportRota.Table[columns.IndexOf(reportRota.NameServico)][row]));
                 tableContent.GetRow(row + 1).GetCell(1).SetParagraph(SetCellText(doc, tableContent, reportRota.Table[columns.IndexOf(reportRota.NameRua)][row]));
                 tableContent.GetRow(row + 1).GetCell(1).SetParagraph(SetCellText(doc, tableContent, reportRota.Table[columns.IndexOf(reportRota.NameNumero)][row]));
                 tableContent.GetRow(row + 1).GetCell(1).SetParagraph(SetCellText(doc, tableContent, reportRota.Table[columns.IndexOf(reportRota.NameBairro)][row]));
                 tableContent.GetRow(row + 1).GetCell(1).SetParagraph(SetCellText(doc, tableContent, reportRota.Table[columns.IndexOf(reportRota.NameCidade)][row]));
                 tableContent.GetRow(row + 1).GetCell(1).SetParagraph(SetCellText(doc, tableContent, reportRota.Table[columns.IndexOf(reportRota.NameComplemento)][row]));
                 tableContent.GetRow(row + 1).GetCell(1).SetParagraph(SetCellText(doc, tableContent, equipes.FirstOrDefault().Nome));
             }*/


            doc.Write(ms);
            ms.Flush();

            return ms.ToArray();
        }

        private XWPFParagraph SetCellText(XWPFDocument doc, XWPFTable table, string setText)
        {
            // Configuración del formato de texto en la tabla  
            CT_P para = new CT_P();
            XWPFParagraph pCell = new XWPFParagraph(para, table.Body);
            pCell.Alignment = ParagraphAlignment.CENTER;// La fuente está centrada  
            pCell.VerticalAlignment = TextAlignment.CENTER;// La fuente está centrada  

            XWPFRun r1c1 = pCell.CreateRun();
            r1c1.SetText(setText);
            r1c1.FontSize = 12;
            r1c1.FontFamily = "Regular chino";
            //r1c1.SetTextPosition(20);//Set the height  
            return pCell;
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

            int countColumns = sheet.GetRow(0).Cells.Select(c => c.StringCellValue).Count();

            var table = new List<List<string>>();

            for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;
                for (int column = row.FirstCellNum; column < countColumns; column++)
                {
                    table.Add(new List<string>());

                    if (row.GetCell(column) == null)
                        table[column].Add(null);
                    else
                        table[column].Add(row.GetCell(column).ToString());

                }
            }

            return table;
        }

        public async Task<RouteExcel> HeaderExcel(IFormFile file)
        {
            RouteExcel reportRota = new RouteExcel();

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

            reportRota.TableHTML.Append("<table class='table table-bordered'><tr>");

            reportRota.Columns = sheet.GetRow(0).Cells.Select(c => c.StringCellValue);

            for (int j = 0; j < reportRota.Columns.Count(); j++)
            {
                reportRota.TableHTML.Append("<th>" + reportRota.Columns.ElementAt(j) + "</th>");
            }

            reportRota.Table = new List<List<string>>();

            reportRota.TableHTML.Append("</tr>");
            reportRota.TableHTML.AppendLine("<tr>");
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;
                for (int column = row.FirstCellNum; column < reportRota.Columns.Count(); column++)
                {
                    reportRota.Table.Add(new List<string>());
                    if (row.GetCell(column) == null)
                    {
                        if (i == 3) reportRota.TableHTML.Append("<td></td>");
                        reportRota.Table[column].Add(null);
                    }
                    else
                    {
                        if (i == 3) reportRota.TableHTML.Append("<td>" + row.GetCell(column).ToString() + "</td>");
                        reportRota.Table[column].Add(row.GetCell(column).ToString());
                    }
                }
                reportRota.TableHTML.AppendLine("</tr>");
            }
            reportRota.TableHTML.Append("</table>");

            return reportRota;
        }


    }
}
