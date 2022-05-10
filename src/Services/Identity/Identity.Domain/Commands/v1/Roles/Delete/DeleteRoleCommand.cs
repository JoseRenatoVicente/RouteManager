using RouteManager.Domain.Core.Contracts;

namespace Identity.Domain.Commands.v1.Roles.Delete;

public sealed record DeleteRoleCommand(string Id) : IBaseCommand;