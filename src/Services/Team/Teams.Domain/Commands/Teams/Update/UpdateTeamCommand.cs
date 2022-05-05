using RouteManager.Domain.Core.Contracts;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.Teams.Update;

public sealed class UpdateTeamCommand : IBaseCommand
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<Person>? People { get; set; }
    public City? City { get; set; }
}