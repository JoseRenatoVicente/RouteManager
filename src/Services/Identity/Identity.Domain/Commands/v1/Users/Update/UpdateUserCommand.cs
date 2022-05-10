using Identity.Domain.Entities.v1;
using RouteManager.Domain.Core.Contracts;

namespace Identity.Domain.Commands.v1.Users.Update;

public sealed record UpdateUserCommand : IBaseCommand
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public bool Active { get; init; } = true;
    public Role? Role { get; init; }
}