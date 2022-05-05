using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.Cities.Create;

public sealed class CreateCityCommand : IBaseCommand
{
    public string? Name { get; set; }
    public string? State { get; set; }
}