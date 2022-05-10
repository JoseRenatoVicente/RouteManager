using Identity.Domain.Entities.v1;
using RouteManager.Domain.Core.Contracts;

namespace Identity.Domain.Commands.v1.Roles.Create;

public sealed record CreateRoleCommand : IBaseCommand
{
    public string? Description { get; init; }
    public IEnumerable<Claim>? Claims { get; init; }
}