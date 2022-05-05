using NPOI.XWPF.UserModel;
using RouteManager.Domain.Core.Handlers;
using RouteManager.Domain.Core.Services;
using RouteManager.Domain.Core.Utils;
using RouteManager.WebAPI.Core.Notifications;
using Routes.Domain.Contracts.v1;
using Routes.Domain.Entities.v1;
using Routes.Domain.Extensions;

namespace Routes.Domain.Commands.ExcelFiles.Report;

public class ReportRouteCommandHandler : CommandHandler<ReportRouteCommand>
{
    private readonly  GatewayService _gatewayService;
    private readonly IExcelFileRepository _excelFileRepository;


    public ReportRouteCommandHandler(INotifier notifier, GatewayService gatewayService, IExcelFileRepository excelFileRepository) : base(notifier)
    {
        _gatewayService = gatewayService;
        _excelFileRepository = excelFileRepository;
    }


    public override async Task<Response> Handle(ReportRouteCommand request, CancellationToken cancellationToken)
    {
        if (!ExecuteValidation(new ReportRouteValidation(), request)) return new Response();

        var excelFile = await _excelFileRepository.FindAsync(file =>  file.Id == request.ExcelFileId);

        request.City = await _gatewayService.GetFromJsonAsync<City>("Teams/api/v1/Cities/" + request.City!.Id);

        excelFile.Table!.RemoveAll(row =>
            !StringUtils.CompareToIgnore(row[excelFile.Columns!.IndexOf(request.NameCity!)], request.City.Name) ||
            !StringUtils.CompareToIgnore(row[excelFile.Columns.IndexOf(request.NameService!)],
                request.TypeService));

        excelFile.Table = excelFile.Table.OrderBy(c => c[excelFile.Columns!.IndexOf(request.NameCEP!)]).ToList();

        if (excelFile.Table == null)
        {
            Notification(
                $"Nenhuma rota correspondente para a Cidade: {request.City.Name} e Serviço: {request.TypeService}");
            return new Response();
        }


        if (excelFile.Table.Count / 5 > request.NameTeams!.Count())
        {
            Notification(
                $"Equipes insuficientes para quantidade de rotas que são {excelFile.Table.Count}. Selecione mais equipes");
            return new Response();
        }

        return new Response
        {
            Entity = await CreateDocx(request, excelFile)
        };
    }

    private Task<byte[]> CreateDocx(ReportRouteCommand reportRoute, ExcelFile excelFile)
    {
        XWPFDocument document = new();
        CreateTitle(document);

        var textLine = document.CreateParagraph().CreateRun();
        textLine.FontSize = 9;
        textLine.SetFontFamily("Calibri", FontCharRange.Ascii);


        var countTeam = reportRoute.NameTeams!.Count();
        var divisor = excelFile.Table!.Count / countTeam;

        List<int> indexTeam = new();
        for (var i = 1; i < countTeam; i++)
            indexTeam.Add(divisor * i);


        textLine.AppendTextLine($"Nome da Equipe: {reportRoute.NameTeams!.ElementAt(0)}");
        for (var row = 0; row < excelFile.Table.Count; row++)
        {
            if (indexTeam.Contains(row))
            {
                textLine.AddBreak(BreakType.PAGE);
                textLine.AppendTextLine(
                    $"Nome da Equipe: {reportRoute.NameTeams!.ElementAt(indexTeam.IndexOf(row) + 1)}");
            }

            var route = new Route
            {
                OS = excelFile.Table[row][excelFile.Columns!.IndexOf(reportRoute.NameOS!)],
                Base = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameBase!)],
                Service = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameService!)],
                Address = new Address
                {
                    Street = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameStreet!)],
                    Number = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameNumber!)],
                    District = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameDistrict!)],
                    CEP = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameCEP!)],
                    Complement = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameComplement!)],
                    City = new City { Name = excelFile.Table[row][excelFile.Columns.IndexOf(reportRoute.NameCity!)] }
                }
            };

            textLine.AppendTextLine($"Endereço: {route.Address}");
            textLine.AppendTextLine($"O.S: {route.OS} - TIPO O.S: {route.Service}");
            textLine.AppendTextLine($"Base: {route.Base}");

            if (reportRoute.ReportColumns != null)
                foreach (var item in reportRoute.ReportColumns)
                    textLine.AppendTextLine($"{item}: {excelFile.Table[row][excelFile.Columns.IndexOf(item)]}");

            textLine.AddBreak(BreakType.TEXTWRAPPING);
        }


        MemoryStream memoryStream = new();
        document.Write(memoryStream);
        memoryStream.Flush();

        return Task.Run(() => memoryStream.ToArray());
    }

    private void CreateTitle(XWPFDocument document)
    {
        var paragraph = document.CreateParagraph();
        var runTitle = paragraph.CreateRun();

        runTitle.Paragraph.Alignment = ParagraphAlignment.CENTER;
        runTitle.FontSize = 14;
        runTitle.IsBold = true;
        runTitle.SetFontFamily("Calibri", FontCharRange.Ascii);
        runTitle.AppendTextLine("ROTA TRABALHO - " + DateTime.UtcNow.ToString("dd/MM/yyyy"));
    }
}