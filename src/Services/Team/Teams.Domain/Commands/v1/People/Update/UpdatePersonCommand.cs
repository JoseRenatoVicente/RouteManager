using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.v1.People.Update;

public sealed record UpdatePersonCommand : IBaseCommand
{
    public string? Id { get; init; }
    public string? Name { get; init; }
}