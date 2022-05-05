using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.Cities.Delete;

public sealed class DeleteCityCommand : IBaseCommand
{
    public string? Id { get; set; }
}