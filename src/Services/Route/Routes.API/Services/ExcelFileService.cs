﻿using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using RouteManager.Domain.Core.Entities.Enums;
using RouteManager.Domain.Core.Services;
using RouteManager.Domain.Core.Services.Base;
using RouteManager.Domain.Core.Utils;
using RouteManager.WebAPI.Core.Notifications;
using Routes.API.DTO;
using Routes.API.Extensions;
using Routes.Domain.Contracts.v1;
using Routes.Domain.Entities.v1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.API.Services
{
    public interface IExcelFileService
    {
        Task<ExcelFile> AddExcelFileAsync(IFormFile file);
        Task<byte[]> CreateDocx(ReportRouteRequest reportRoute, ExcelFile excelFile);
        Task<IEnumerable<ExcelFile>> GetExcelFilesAsync();
        Task<ExcelFile> GetExcelFileByIdAsync(string id);
        Task<List<List<string>>> GetTableExcel(IFormFile file);
        Task RemoveExcelFileAsync(ExcelFile route);
        Task<bool> RemoveExcelFileAsync(string id);
        Task<byte[]> ReportRoutesToDocx(ReportRouteRequest reportRoute);
    }

    public class ExcelFileService : BaseService, IExcelFileService
    {
        private readonly GatewayService _gatewayService;
        private readonly IExcelFileRepository _excelFileRepository;
        private readonly IRouteRepository _routeRepository;

        public ExcelFileService(GatewayService gatewayService, IExcelFileRepository excelFileRepository, IRouteRepository routeRepository, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
            _excelFileRepository = excelFileRepository;
            _routeRepository = routeRepository;
        }

        public async Task<IEnumerable<ExcelFile>> GetExcelFilesAsync() =>
           await _excelFileRepository.GetAllAsync();

        public async Task<ExcelFile> GetExcelFileByIdAsync(string id) =>
            await _excelFileRepository.FindAsync(c => c.Id == id);

        public async Task<ExcelFile> AddExcelFileAsync(IFormFile file)
        {
            var excelFile = new ExcelFile();
            excelFile.Table = await GetTableExcel(file);
            excelFile.FileName = file.FileName;
            excelFile.Columns = excelFile.Table.FirstOrDefault();
            excelFile.Table.RemoveAt(0);

            await _gatewayService.PostLogAsync(null, excelFile, Operation.Create);
            return await _excelFileRepository.AddAsync(excelFile);

        }
        public async Task RemoveExcelFileAsync(ExcelFile route)
        {
            await _gatewayService.PostLogAsync(null, route, Operation.Delete);
            await _excelFileRepository.RemoveAsync(route);
        }

        public async Task<bool> RemoveExcelFileAsync(string id)
        {
            var route = await GetExcelFileByIdAsync(id);
            if (route == null)
            {
                Notification("Rota não encontrada");
                return false;
            }

            await _gatewayService.PostLogAsync(null, route, Operation.Delete);
            return await _excelFileRepository.RemoveAsync(id);
        }


        public async Task<byte[]> ReportRoutesToDocx(ReportRouteRequest reportRoute)
        {
            if (!ExecuteValidation(new ReportRouteRequestValidation(), reportRoute)) return null;

            var excelFile = await GetExcelFileByIdAsync(reportRoute.ExcelFileId);

            reportRoute.City = await _gatewayService.GetFromJsonAsync<City>("Teams/api/v1/Cities/" + reportRoute.City.Id);

            excelFile.Table.RemoveAll(row => !StringUtils.CompareToIgnore(row[excelFile.Columns.IndexOf(reportRoute.NameCity)], reportRoute.City.Name) ||
                                             !StringUtils.CompareToIgnore(row[excelFile.Columns.IndexOf(reportRoute.NameService)], reportRoute.TypeService));

            excelFile.Table = excelFile.Table.OrderBy(c => c[excelFile.Columns.IndexOf(reportRoute.NameCEP)]).ToList();

            if (excelFile.Table == null)
            {
                Notification($"Nenhuma rota correspondente para a Cidade: {reportRoute.City.Name} e Serviço: {reportRoute.TypeService}");
                return null;
            }


            if (excelFile.Table.Count() / 5 > reportRoute.NameTeams.Count())
            {
                Notification($"Equipes insuficientes para quantidade de rotas que são {excelFile.Table.Count()}. Selecione mais equipes");
                return null;
            }

            return await CreateDocx(reportRoute, excelFile);
        }

        public Task<byte[]> CreateDocx(ReportRouteRequest reportRoute, ExcelFile excelFile)
        {
            XWPFDocument document = new();
            CreateTitle(document);

            XWPFRun textLine = document.CreateParagraph().CreateRun();
            textLine.FontSize = 9;
            textLine.SetFontFamily("Calibri", FontCharRange.Ascii);


            int countTeam = reportRoute.NameTeams.Count();
            int divisao = excelFile.Table.Count() / countTeam;

            List<int> indexTeam = new();
            for (int i = 1; i < countTeam; i++)
                indexTeam.Add(divisao * i);


            textLine.AppendTextLine($"Nome da Equipe: {reportRoute.NameTeams.ElementAt(0)}");
            for (int row = 0; row < excelFile.Table.Count(); row++)
            {
                if (indexTeam.Contains(row))
                {
                    textLine.AddBreak(BreakType.PAGE);
                    textLine.AppendTextLine($"Nome da Equipe: {reportRoute.NameTeams.ElementAt(indexTeam.IndexOf(row) + 1)}");
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

                textLine.AppendTextLine($"Endereço: {route.Address}");
                textLine.AppendTextLine($"O.S: {route.OS} - TIPO O.S: {route.Service}");
                textLine.AppendTextLine($"Base: {route.Base}");

                if (reportRoute.ReportColumns != null)
                    foreach (var item in reportRoute.ReportColumns)
                        if (item != null) textLine.AppendTextLine($"{item}: {excelFile.Table[row][excelFile.Columns.IndexOf(item)]}");

                textLine.AddBreak(BreakType.TEXTWRAPPING);
            }


            MemoryStream memoryStream = new();
            document.Write(memoryStream);
            memoryStream.Flush();

            return Task.Run(() => memoryStream.ToArray());
        }


        public void CreateTitle(XWPFDocument document)
        {
            var paragraph = document.CreateParagraph();
            XWPFRun runTitle = paragraph.CreateRun();

            runTitle.Paragraph.Alignment = ParagraphAlignment.CENTER;
            runTitle.FontSize = 14;
            runTitle.IsBold = true;
            runTitle.SetFontFamily("Calibri", FontCharRange.Ascii);
            runTitle.AppendTextLine("ROTA TRABALHO - " + DateTime.UtcNow.ToString("dd/MM/yyyy"));
        }


        public Task<List<List<string>>> GetTableExcel(IFormFile file)
        {
            ISheet sheet;

            if (Path.GetExtension(file.FileName).ToLower() == ".xls")
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

            int numberRows = sheet.GetRow(0).Count();

            for (int indexRow = 0; indexRow <= sheet.LastRowNum; indexRow++)
            {
                IRow row = sheet.GetRow(indexRow);

                table.Add(new List<string>());

                for (int column = 0; column < numberRows; column++)
                    table[indexRow].Add(row.GetCell(column) == null ? null : row.GetCell(column).ToString());
            }

            return Task.Run(() => table);
        }
    }
}
