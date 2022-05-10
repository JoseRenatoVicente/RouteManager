using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.v1.People.Delete;

public sealed record DeletePersonCommand(string Id) : IBaseCommand;