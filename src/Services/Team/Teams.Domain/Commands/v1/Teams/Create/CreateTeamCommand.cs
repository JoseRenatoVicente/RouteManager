using RouteManager.Domain.Core.Contracts;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.Teams.Create;

public sealed record CreateTeamCommand : IBaseCommand
{
    public string? Name { get; init; }
    public IEnumerable<Person>? People { get; init; }
    public City? City { get; init; }
}