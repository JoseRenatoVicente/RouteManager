using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.v1.Cities.Create;

public sealed record CreateCityCommand : IBaseCommand
{
    public string? Name { get; init; }
    public string? State { get; init; }
}