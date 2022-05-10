using RouteManager.Domain.Core.Entities.Base;

namespace Routes.Domain.Entities.v1;

public sealed class ExcelFile : EntityBase
{
    public DateTime UploadDate { get; set; } = DateTime.Now;
    public string? FileName { get; set; }
    public List<string>? Columns { get; set; }
    public List<List<string?>>? Table { get; set; }
}