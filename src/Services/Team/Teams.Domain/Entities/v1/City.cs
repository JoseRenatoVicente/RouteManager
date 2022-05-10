using RouteManager.Domain.Core.Entities.Base;

namespace Teams.Domain.Entities.v1;

public sealed class City : EntityBase
{
    public string? Name { get; set; }
    public string? State { get; set; }

}