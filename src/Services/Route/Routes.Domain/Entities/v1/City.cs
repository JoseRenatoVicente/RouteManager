using RouteManager.Domain.Core.Entities.Base;

namespace Routes.Domain.Entities.v1;

public class City : EntityBase
{
    public string? Name { get; init; }
    public string? State { get; set; }

}