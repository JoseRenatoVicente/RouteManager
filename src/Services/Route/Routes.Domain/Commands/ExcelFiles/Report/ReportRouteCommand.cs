using RouteManager.Domain.Core.Contracts;
using Routes.Domain.Entities.v1;

namespace Routes.Domain.Commands.ExcelFiles.Report;

public sealed class ReportRouteCommand : IBaseCommand
{
    public string? NameOS { get; set; }
    public string? NameBase { get; set; }
    public string? NameService { get; set; }
    public string? NameStreet { get; set; }
    public string? NameNumber { get; set; }
    public string? NameComplement { get; set; }
    public string? NameDistrict { get; set; }
    public string? NameCEP { get; set; }
    public string? NameCity { get; set; }

    public string? TypeService { get; set; }
    public City? City { get; set; }

    public string? ExcelFileId { get; set; }

    public IEnumerable<string>? ReportColumns { get; set; }

    public IEnumerable<string>? NameTeams { get; set; }
}