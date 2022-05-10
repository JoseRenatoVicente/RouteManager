using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.v1.Cities.Delete;

public sealed record DeleteCityCommand(string Id) : IBaseCommand;