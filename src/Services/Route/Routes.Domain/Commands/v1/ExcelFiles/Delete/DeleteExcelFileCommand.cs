using RouteManager.Domain.Core.Contracts;

namespace Routes.Domain.Commands.v1.ExcelFiles.Delete;

public sealed record DeleteExcelFileCommand(string Id) : IBaseCommand;