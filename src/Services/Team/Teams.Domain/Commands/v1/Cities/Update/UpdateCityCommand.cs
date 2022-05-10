using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.v1.Cities.Update;

public sealed record UpdateCityCommand : IBaseCommand
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? State { get; init; }
}