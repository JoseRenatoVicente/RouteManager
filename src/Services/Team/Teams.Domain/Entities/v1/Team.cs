using RouteManager.Domain.Core.Entities.Base;

namespace Teams.Domain.Entities.v1;

public sealed class Team : EntityBase
{
    public string? Name { get; set; }
    public IEnumerable<Person>? People { get; set; }
    public City? City { get; set; }
}