using Identity.Domain.Entities.v1;
using RouteManager.Domain.Core.Contracts;

namespace Identity.Domain.Commands.Users.Update;

public sealed class UpdateUserCommand : IBaseCommand
{
    public string? Id { get; set; }
    public string? Name { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public bool Active { get; set; } = true;
    public Role? Role { get; set; }
}