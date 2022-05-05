using RouteManager.Domain.Core.Contracts;
using System.Security.Claims;

namespace Identity.Domain.Commands.Roles.Create;

public sealed class CreateRoleCommand : IBaseCommand
{
    public string? Description { get; set; }
    public IEnumerable<Claim>? Claims { get; set; }
}