using Microsoft.AspNetCore.Http;
using RouteManager.Domain.Core.Contracts;

namespace Routes.Domain.Commands.v1.ExcelFiles.Upload;

public sealed record UploadExcelFileCommand(IFormFile? File) : IBaseCommand;