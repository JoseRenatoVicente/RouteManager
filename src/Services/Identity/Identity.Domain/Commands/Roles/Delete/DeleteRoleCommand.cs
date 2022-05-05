using RouteManager.Domain.Core.Contracts;

namespace Identity.Domain.Commands.Roles.Delete;

public sealed class DeleteRoleCommand : IBaseCommand
{
    public string? Id { get; set; }
}