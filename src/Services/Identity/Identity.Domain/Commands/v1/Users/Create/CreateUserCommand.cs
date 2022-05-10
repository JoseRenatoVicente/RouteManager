using Identity.Domain.Entities.v1;
using RouteManager.Domain.Core.Contracts;

namespace Identity.Domain.Commands.v1.Users.Create;

public sealed record CreateUserCommand : IBaseCommand
{
    public string? Name { get; init; }
    public string? UserName { get; init; }
    public string? Password { get; init; }
    public string? Email { get; init; }
    public Role? Role { get; init; }
}