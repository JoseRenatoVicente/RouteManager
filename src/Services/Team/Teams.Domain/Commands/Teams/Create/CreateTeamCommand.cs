using RouteManager.Domain.Core.Contracts;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.Teams.Create;

public sealed class CreateTeamCommand : IBaseCommand
{
    public string? Name { get; set; }
    public IEnumerable<Person>? People { get; set; }
    public City? City { get; set; }
}