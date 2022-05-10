using RouteManager.Domain.Core.Contracts;
using Routes.Domain.Entities.v1;

namespace Routes.Domain.Commands.v1.ExcelFiles.Report;

public sealed record ReportRouteCommand : IBaseCommand
{
    public string? NameOS { get; init; }
    public string? NameBase { get; init; }
    public string? NameService { get; init; }
    public string? NameStreet { get; init; }
    public string? NameNumber { get; init; }
    public string? NameComplement { get; init; }
    public string? NameDistrict { get; init; }
    public string? NameCEP { get; init; }
    public string? NameCity { get; init; }

    public string? TypeService { get; init; }
    public City? City { get; set; }

    public string? ExcelFileId { get; init; }

    public IEnumerable<string>? ReportColumns { get; init; }

    public IEnumerable<string>? NameTeams { get; init; }
}