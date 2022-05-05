using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.Cities.Update;

public sealed class UpdateCityCommand : IBaseCommand
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? State { get; set; }
}