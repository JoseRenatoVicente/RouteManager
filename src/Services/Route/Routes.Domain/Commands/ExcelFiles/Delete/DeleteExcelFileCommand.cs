using RouteManager.Domain.Core.Contracts;

namespace Identity.Domain.Commands.Roles.Delete;

public sealed class DeleteExcelFileCommand : IBaseCommand
{
    public string? Id { get; set; }
}