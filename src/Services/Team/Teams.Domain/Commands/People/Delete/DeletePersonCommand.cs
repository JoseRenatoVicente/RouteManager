using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.People.Delete;

public sealed class DeletePersonCommand : IBaseCommand
{
    public string? Id { get; set; }
}