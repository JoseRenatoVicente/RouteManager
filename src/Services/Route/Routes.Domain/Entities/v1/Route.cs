using RouteManager.Domain.Core.Entities.Base;

namespace Routes.Domain.Entities.v1;

public class Route : EntityBase
{
    public string? OS { get; init; }
    public string? Base { get; init; }
    public string? Service { get; init; }
    public Address? Address { get; init; }
}