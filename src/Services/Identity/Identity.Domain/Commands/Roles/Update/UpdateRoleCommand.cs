using RouteManager.Domain.Core.Contracts;
using System.Security.Claims;

namespace Identity.Domain.Commands.Roles.Update;

public sealed class UpdateRoleCommand : IBaseCommand
{
    public string? Id { get; set; }
    public string? Description { get; set; }
    public IEnumerable<Claim>? Claims { get; set; }
}