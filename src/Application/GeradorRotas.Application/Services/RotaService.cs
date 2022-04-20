using RouteManager.Application.Services.Interfaces;
using RouteManager.Application.ViewModels;
using RouteManager.Domain.Entities;
using RouteManager.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteManager.Application.Services
{
    public class RotaService : IRotaService
    {
        private readonly IEquipeRepository _equipeRepository;
        private readonly IRotaRepository _rotaRepository;

        public RotaService(IEquipeRepository equipeRepository, IRotaRepository rotaRepository)
        {
            _equipeRepository = equipeRepository;
            _rotaRepository = rotaRepository;
        }

        public async Task<IEnumerable<Rota>> GetRotasAsync() =>
            await _rotaRepository.GetAllAsync();

        public async Task<Rota> GetRotaByIdAsync(string id) =>
            await _rotaRepository.GetByIdAsync(id);

        public async Task AddRotaAsync(Rota rota)
        {
            await _rotaRepository.AddAsync(rota);
        }

        public async Task UpdateRotaAsync(Rota rota)
        {
            await _rotaRepository.UpdateAsync(rota);
        }

        public async Task RemoveRotaAsync(string id)
        {
            await _rotaRepository.DeleteAsync(await GetRotaByIdAsync(id));
        }

        public async Task<byte[]> ExportToDocx(ReportRotaViewModel reportRota)
        {
            List<string> columns = reportRota.Columns.ToList();
            int numberRows = reportRota.Table[0].Count;
            IEnumerable<Equipe> equipes = await _equipeRepository.GetAllAsync();
            StringBuilder stringBuilder = new StringBuilder();

            for (int row = 0; row < numberRows; row++)
            {
                var rota = new Rota
                {
                    OS = reportRota.Table[columns.IndexOf(reportRota.NameOS)][row],
                    Base = reportRota.Table[columns.IndexOf(reportRota.NameBase)][row],
                    Servico = reportRota.Table[columns.IndexOf(reportRota.NameServico)][row],
                    Cidade = new City { Name = reportRota.Table[columns.IndexOf(reportRota.NameCidade)][row] },
                    Endereco = new Endereco
                    {
                        Rua = reportRota.Table[columns.IndexOf(reportRota.NameRua)][row],
                        Numero = reportRota.Table[columns.IndexOf(reportRota.NameNumero)][row],
                        Bairro = reportRota.Table[columns.IndexOf(reportRota.NameBairro)][row],
                        CEP = reportRota.Table[columns.IndexOf(reportRota.NameCEP)][row],
                        Complemento = reportRota.Table[columns.IndexOf(reportRota.NameBairro)][row]
                    },
                };

                await _rotaRepository.AddAsync(rota);

                if (reportRota.Table[columns.IndexOf(reportRota.NameOS)][row] != null) stringBuilder.AppendLine($"{reportRota.NameOS}: {reportRota.Table[columns.IndexOf(reportRota.NameOS)][row]}\n");
                stringBuilder.AppendLine($"{reportRota.NameServico}: {reportRota.Table[columns.IndexOf(reportRota.NameServico)][row]}\n");
                stringBuilder.AppendLine($"{reportRota.NameRua}: {reportRota.Table[columns.IndexOf(reportRota.NameRua)][row]}\n");
                stringBuilder.AppendLine($"{reportRota.NameBase}: {reportRota.Table[columns.IndexOf(reportRota.NameBase)][row]}\n");
                stringBuilder.AppendLine($"{reportRota.NameNumero}: {reportRota.Table[columns.IndexOf(reportRota.NameNumero)][row]}\n");
                stringBuilder.AppendLine($"{reportRota.NameBairro}: {reportRota.Table[columns.IndexOf(reportRota.NameBairro)][row]}\n");
                stringBuilder.AppendLine($"{reportRota.NameCidade}: {reportRota.Table[columns.IndexOf(reportRota.NameCidade)][row]}\n");
                stringBuilder.AppendLine($"{reportRota.NameComplemento}: {reportRota.Table[columns.IndexOf(reportRota.NameComplemento)][row]}\n");
                stringBuilder.AppendLine($"{reportRota.NameCEP}: {reportRota.Table[columns.IndexOf(reportRota.NameCEP)][row]}\n");
                stringBuilder.Append("\n");
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

        public XWPFParagraph SetCellText(XWPFDocument doc, XWPFTable table, string setText)
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

        public async Task<ReportRotaViewModel> RotaUpload(IFormFile file)
        {
            ReportRotaViewModel reportRota = new ReportRotaViewModel();

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
