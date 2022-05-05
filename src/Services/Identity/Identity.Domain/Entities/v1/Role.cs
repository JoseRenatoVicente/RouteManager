using RouteManager.Domain.Core.Entities.Base;

namespace Identity.Domain.Entities.v1;

public class Role : EntityBase
{
    public string? Description { get; init; }
    public IEnumerable<Claim>? Claims { get; init; }
}