using Microsoft.AspNetCore.Http;
using RouteManager.Domain.Core.Contracts;

namespace Routes.Domain.Commands.ExcelFiles.Upload;

public sealed class UploadExcelFileCommand : IBaseCommand
{
    public IFormFile? File { get; set; }      
}