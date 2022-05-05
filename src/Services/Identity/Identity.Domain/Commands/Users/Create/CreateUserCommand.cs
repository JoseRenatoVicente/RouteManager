using Identity.Domain.Entities.v1;
using RouteManager.Domain.Core.Contracts;

namespace Identity.Domain.Commands.Users.Create;

public sealed class CreateUserCommand : IBaseCommand
{
    public string? Name { get; init; }
    public string? UserName { get; init; }
    public string? Password { get; set; }
    public string? Email { get; init; }
    public Role? Role { get; set; }
}