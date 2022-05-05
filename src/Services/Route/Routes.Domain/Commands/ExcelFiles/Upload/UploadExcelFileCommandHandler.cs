using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Routes.Domain.Contracts.v1;
using Routes.Domain.Entities.v1;

namespace Routes.Domain.Commands.ExcelFiles.Upload;

public sealed class UploadExcelFileCommandHandler : CommandHandler<UploadExcelFileCommand>
{
    private readonly IExcelFileRepository _excelFileRepository;

    public UploadExcelFileCommandHandler(INotifier notifier, IExcelFileRepository excelFileRepository) : base(notifier)
    {
        _excelFileRepository = excelFileRepository;
    }

    public override async Task<Response> Handle(UploadExcelFileCommand request, CancellationToken cancellationToken)
    {
        var excelFile = new ExcelFile
        {
            Table = await GetTableExcel(request.File),
            FileName = request.File!.FileName
        };
        excelFile.Columns = excelFile.Table.FirstOrDefault()!;
        excelFile.Table.RemoveAt(0);

        await _excelFileRepository.AddAsync(excelFile);

        return new Response();
    }


    private Task<List<List<string?>>> GetTableExcel(IFormFile? file)
    {
        ISheet sheet;

        if (Path.GetExtension(file!.FileName).ToLower() == ".xls")
        {
            var workbook = new HSSFWorkbook(file.OpenReadStream()); //This will read the Excel 97-2000 formats  
            sheet = workbook.GetSheetAt(0);
        }
        else
        {
            var workbook = new XSSFWorkbook(file.OpenReadStream()); //This will read 2007 Excel format  
            sheet = workbook.GetSheetAt(0);
        }

        var table = new List<List<string?>>();

        var numberRows = sheet.GetRow(0).Count();

        for (var indexRow = 0; indexRow <= sheet.LastRowNum; indexRow++)
        {
            var row = sheet.GetRow(indexRow);

            table.Add(new List<string?>());

            for (var column = 0; column < numberRows; column++)
                table[indexRow].Add(row.GetCell(column) == null ? null : row.GetCell(column).ToString());
        }

        return Task.Run(() => table);
    }

}