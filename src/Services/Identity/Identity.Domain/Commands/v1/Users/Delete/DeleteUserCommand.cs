using RouteManager.Domain.Core.Contracts;

namespace Identity.Domain.Commands.v1.Users.Delete;

public sealed record DeleteUserCommand(string Id) : IBaseCommand;