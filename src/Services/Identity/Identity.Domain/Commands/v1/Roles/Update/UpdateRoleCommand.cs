using Identity.Domain.Entities.v1;
using RouteManager.Domain.Core.Contracts;

namespace Identity.Domain.Commands.v1.Roles.Update;

public sealed record UpdateRoleCommand : IBaseCommand
{
    public string? Id { get; init; }
    public string? Description { get; init; }
    public IEnumerable<Claim>? Claims { get; init; }
}