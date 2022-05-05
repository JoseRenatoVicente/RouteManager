using RouteManager.Domain.Core.Contracts;

namespace Identity.Domain.Commands.Users.Delete;

public sealed class DeleteUserCommand : IBaseCommand
{
    public string? Id { get; set; }
}